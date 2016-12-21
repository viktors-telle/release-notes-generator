using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ReleaseNotesGenerator.Domain.Commit;

namespace ReleaseNotesGenerator.Core.RepositoryHandlers
{
    public class GitRepositoryHandler : IRepositoryHandler
    {
        public async Task<IList<Commit>> GetCommits(CommitQuery query)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "api-version", "1.0" },
                { "branch", query.BranchName }
            };

            if (query.DateTime.HasValue)
            {
                queryParameters.Add("fromDate", query.DateTime.Value.AddSeconds(1).ToString("yyyy-MM-ddTHH:mm:ssZ"));
            }
            
            var repositoryUrl = QueryHelpers.AddQueryString(new Uri(new Uri(query.Url, UriKind.Absolute), $"{query.RepositoryName}/commits").ToString(), queryParameters);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.Encoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", query.AccessToken))));

                var response = await httpClient.GetAsync(repositoryUrl);
                var responseContent = await response.Content.ReadAsStringAsync();
                var commitResponse = JsonConvert.DeserializeObject<CommitResponse>(responseContent);
                return commitResponse.Value;
            }
        }
    }
}
