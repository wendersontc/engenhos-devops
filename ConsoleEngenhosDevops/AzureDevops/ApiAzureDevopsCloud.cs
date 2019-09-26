using System.Linq;
using System;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleEngenhosDevops.Models;
using Microsoft.VisualStudio.Services.Common;

namespace ConsoleEngenhosDevops.AzureDevops
{
    public class ApiAzureDevopsCloud
    {
        readonly string _url;
        readonly string _basicToken;
        readonly string _project;

        public ApiAzureDevopsCloud(string url, string token, string project){
           _url = url;
           _basicToken = token;
           _project = project;
        }

        public async Task<List<WorkItem>> getWorkItens(){
            Uri url = new Uri(_url);

            VssBasicCredential login = new VssBasicCredential(null,_basicToken);

            Wiql wiql = new Wiql(){
                 Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        "Where [System.TeamProject] = '" + _project + "' "
            };
            using (WorkItemTrackingHttpClient workHttp = new WorkItemTrackingHttpClient(url,login)){
                WorkItemQueryResult workItemQueryResult = await workHttp.QueryByWiqlAsync(wiql);
                if (workItemQueryResult.WorkItems.Count() != 0)
                    {
                        List<int> list = new List<int>();
                        foreach (var item in workItemQueryResult.WorkItems)
                        {
                            list.Add(item.Id);
                        }
                        int[] arr = list.ToArray();

                        string[] fields = new string[5];
                        fields[0] = "System.Id";
                        fields[1] = "System.Title";
                        fields[2] = "System.State";
                        fields[3] = "System.WorkItemType";
                        fields[4] = "System.ChangedDate";

                        var workItemsResult = await workHttp.GetWorkItemsAsync(arr, fields, workItemQueryResult.AsOf);
      
                        return workItemsResult;
                    } 
            }
            return null;
        }
    }
}