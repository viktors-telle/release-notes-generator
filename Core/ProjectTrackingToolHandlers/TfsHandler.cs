using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Core.ProjectTrackingToolHandlers
{
    public class TfsHandler : IProjectTrackingToolHandler
    {
        public async Task<WorkItem> GetWorkItems(ProjectTrackingTool projectTrackingTool, IEnumerable<string> workItemIds)
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
                            string.Format("{0}:{1}", "", "accessToken"))));

                var response = await httpClient.GetAsync(repositoryUrl);
                var responseContent = await response.Content.ReadAsStringAsync();
                return new WorkItem();
                //var commitResponse = JsonConvert.DeserializeObject<CommitResponse>(responseContent);
                //return commitResponse.Value;
            }
        }
    }
}