using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Domain;
using ReleaseNotes.Generator.Domain.WorkItem;

namespace ReleaseNotes.Generator.Components.Implementations
{
    public class TfsHandler : IProjectTrackingToolHandler
    {
        private const string WorkItemLinkSeparator = "<br />";

        public async Task<IList<WorkItem>> GetWorkItems(ProjectTrackingTool projectTrackingTool,
            string[] workItemIds)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"api-version", "1.0"},                
                {"fields", "System.Title,System.State" }
            };          

            using (var httpClient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true }))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var tasks =
                    workItemIds.Select(id => GetWorkItem(id, projectTrackingTool.Url, httpClient, queryParameters)).ToList();
                var workItems = (await Task.WhenAll(tasks)).ToList();
                return workItems
                    .Where(workItem => workItem != null)
                    .Where(workItem => 
                        workItem.Field.State.Equals("done", StringComparison.InvariantCultureIgnoreCase)
                        || workItem.Field.State.Equals("released", StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
            }
        }

        private async Task<WorkItem> GetWorkItem(string id, string projectTrackingToolUrl, HttpClient httpClient, IDictionary<string, string> queryParameters)
        {
            var workItemUrl =
                QueryHelpers.AddQueryString(
                    new Uri(new Uri(projectTrackingToolUrl, UriKind.Absolute), $"_apis/wit/workitems/{id}").ToString(),
                    queryParameters);

            var response = await httpClient.GetAsync(workItemUrl);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var workItemResponse = JsonConvert.DeserializeObject<WorkItem>(responseContent);
                return workItemResponse;
            }

            return null;
        }

        public string CreateLinksToWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<WorkItem> workItems)
        {
            var links = new List<string>();
            foreach (var workItem in workItems)
            {
                var queryParameters = new Dictionary<string, string>
                {
                    {"_a", "edit"},
                    {"id", workItem.Id.ToString()}
                };

                var workItemUrl =
                    $@"<a href='{QueryHelpers.AddQueryString(new Uri(new Uri(projectTrackingTool.Url, UriKind.Absolute),
                        $"{projectTrackingTool.ProjectName}/_workitems").ToString(),
                        queryParameters)}'>{workItem.Field.Title}</a>";
                links.Add(workItemUrl);
            }

            var linksWithSeparator = string.Join(WorkItemLinkSeparator, links);
            return linksWithSeparator;
        }
    }
}