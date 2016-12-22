using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ReleaseNotesGenerator.Domain;
using ReleaseNotesGenerator.Domain.WorkItem;

namespace ReleaseNotesGenerator.Core.ProjectTrackingToolHandlers
{
    public class TfsHandler : IProjectTrackingToolHandler
    {
        private const string WorkItemLinkSeparator = "<br />";
        public async Task<IList<WorkItem>> GetWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<string> workItemIds)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "api-version", "1.0" },                
                { "ids", workItemIds.Aggregate((a, b) => a + "," + b) },                
            };

            var repositoryUrl = QueryHelpers.AddQueryString(new Uri(new Uri(projectTrackingTool.Url, UriKind.Absolute), "_apis/wit/workitems").ToString(), queryParameters);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.Encoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", projectTrackingTool.AccessToken))));

                var response = await httpClient.GetAsync(repositoryUrl);
                var responseContent = await response.Content.ReadAsStringAsync();
                var workItemResponse = JsonConvert.DeserializeObject<WorkItemResponse>(responseContent);
                return workItemResponse.Value;
            }
        }

        public string CreateLinksToWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<WorkItem> workItems)
        {
            var links = new List<string>();
            foreach (var workItem in workItems)
            {
                var workItemUrl = $@"<a href='{QueryHelpers.AddQueryString(new Uri(new Uri(projectTrackingTool.Url, UriKind.Absolute), 
                    $"{projectTrackingTool.ProjectName}/_workitems").ToString(), 
                    "id", 
                    workItem.Id.ToString())}'>{workItem.Field.Title}</a>";                            
                links.Add(workItemUrl);
            }

            var linksWithSeparator = string.Join(WorkItemLinkSeparator, links);
            return linksWithSeparator;            
        }
    }
}