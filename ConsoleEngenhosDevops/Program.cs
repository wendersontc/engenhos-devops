using System.Linq;
using System.Threading;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ConsoleEngenhosDevops.Settings;
using ConsoleEngenhosDevops.Services;
using ConsoleEngenhosDevops.Models;
using ConsoleEngenhosDevops.AzureDevops;
using Newtonsoft.Json;

namespace engenhoAzureDevops
{
    class Program
    {
        public static Timer _timer;
        private static ServiceConfigurations _configurations;
        private static AutoResetEvent waitHandle = new AutoResetEvent(false);

        public static void TimerElapsed(object state)
        {
            
            WorkItemService service = new WorkItemService(_configurations);

            var itens = new ApiAzureDevopsCloud(_configurations.Url
                           ,_configurations.BasicToken,_configurations.Project).getWorkItens();

            while (!itens.IsCompleted);

             foreach (var item in itens.Result.ToList())
                {
                    var workItem = new WorkItemResult
                    {
                        IdWorkItem = item.Id.Value
                    };

                    foreach(var valor in item.Fields)
                    {
                        if(valor.Key == "System.Title")
                        workItem.Titulo = valor.Value.ToString();

                        if (valor.Key == "System.WorkItemType")
                            workItem.Tipo = valor.Value.ToString();

                        if (valor.Key == "System.ChangedDate")
                            workItem.DataCriacaoWorkItem = DateTime.Parse(valor.Value.ToString());
                    }
                    if(!service.GetByWorkId(item.Id.Value)){
                        service.Create(workItem);
                    }

                    Console.WriteLine(JsonConvert.SerializeObject(workItem));
                    
                }
        
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando console Aplication Engenhos Devops");

            Console.WriteLine("Carregando configurações...");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            var configuration = builder.Build();

            _configurations = new ServiceConfigurations();
            new ConfigureFromConfigurationOptions<ServiceConfigurations>(
                configuration.GetSection("ServiceConfigurations"))
                    .Configure(_configurations);

            _timer = new Timer(
                 callback: TimerElapsed,
                 state: null,
                 dueTime: 0,
                 period: 3000);

            // Tratando o encerramento da aplicação com
            // Control + C ou Control + Break
            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Saindo...");

                // Libera a continuação da thread principal
                waitHandle.Set();
            };     
            
            waitHandle.WaitOne();
        }
    }
}
