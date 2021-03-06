﻿using System.ComponentModel;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ApiEngenhosDevops.Settings;
using ApiEngenhosDevops.Services;

namespace ApiEngenhosDevops
{
    public class Startup
    {
       public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DevopsStoreSettings>(
                Configuration.GetSection(nameof(DevopsStoreSettings)));

            services.AddSingleton<IBookstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DevopsStoreSettings>>().Value);

            services.AddSingleton<WorkItemService>();    

            services.AddMvc()
                   
                   .AddJsonOptions(options => options.UseMemberCasing())
                   .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();       
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseCors(builder =>builder.AllowAnyOrigin());
            app.UseMvc();
        }
    }
}
