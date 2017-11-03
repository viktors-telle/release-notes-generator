using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Domain.Commit;

namespace ReleaseNotes.Generator.Components.Implementations
{
    public class GitRepositoryHandler : IRepositoryHandler
    {
        public async Task<IList<Commit>> GetCommits(CommitQuery query)
        {
            const int top = 100;
            var commits = new List<Commit>();
            var commitCount = 100;
            var skip = 0;            

            var queryParameters = new Dictionary<string, string>
            {
                { "api-version", "1.0" },
                { "branch", query.BranchName },
                { "$skip", skip.ToString() },
                { "$top", top.ToString() },
                { "toDate", DateTime.Now.ToString("s") + "Z" }
            };

            if (!string.IsNullOrEmpty(query.ItemPath))
            {
                queryParameters.Add("itemPath", query.ItemPath);
                var commitDateTime = await GetCommitDateTime(query);
                queryParameters.Add("fromDate", commitDateTime.AddSeconds(1).ToString("s") + "Z");
            }
            else
            {
                if (!string.IsNullOrEmpty(query.LastCommitId) && query.DateTime.HasValue)
                {
                    queryParameters.Add("fromDate", query.DateTime.Value.AddSeconds(1).ToString("s") + "Z");
                }
                else if (!string.IsNullOrEmpty(query.LastCommitId) && !query.DateTime.HasValue)
                {
                    var commitDateTime = await GetCommitDateTime(query);
                    queryParameters.Add("fromDate", commitDateTime.AddSeconds(1).ToString("s") + "Z");
                }
            }            
             
            while (commitCount == 100)
            {
                queryParameters["$skip"] = skip.ToString();
                var repositoryUrl = QueryHelpers.AddQueryString(
                    new Uri(new Uri(query.Url, UriKind.Absolute), $"_apis/git/repositories/{query.RepositoryName}/commits").ToString(), queryParameters);

                using (var httpClient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true }))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await httpClient.GetAsync(repositoryUrl);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var commitResponse = JsonConvert.DeserializeObject<CommitResponse>(responseContent);
                    commitCount = commitResponse.Count;
                    skip += 100;                    
                    commits.AddRange(commitResponse.Value);
                }
            }

            return commits;
        }

        public async Task<IEnumerable<Commit>> GetCommitsWithFullComments(CommitQuery query, IEnumerable<Commit> commits)
        {
            List<Commit> commitsWithFullComments;
            var queryParameters = new Dictionary<string, string>
            {
                { "api-version", "1.0" }
            };

            using (var httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));                

                var tasks =
                    commits.Select(c => GetCommitWithFullComment(query, c, queryParameters, httpClient)).ToList();
                commitsWithFullComments = (await Task.WhenAll(tasks)).ToList();
            }

            return commitsWithFullComments;
        }

        private async Task<Commit> GetCommitWithFullComment(CommitQuery query, Commit commit, IDictionary<string, string> queryParameters, HttpClient httpClient)
        {
            var repositoryUrl = QueryHelpers.AddQueryString(new Uri(new Uri(query.Url, UriKind.Absolute),
                       $"_apis/git/repositories/{query.RepositoryName}/commits/{commit.CommitId}").ToString(), queryParameters);
            var response = await httpClient.GetAsync(repositoryUrl);
            var responseContent = await response.Content.ReadAsStringAsync();
            var commitResponse = JsonConvert.DeserializeObject<Commit>(responseContent);
            return commitResponse;
        }

        private async Task<DateTime> GetCommitDateTime(CommitQuery query)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "api-version", "1.0" }
            };

            using (var httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var repositoryUrl = QueryHelpers.AddQueryString(new Uri(new Uri(query.Url, UriKind.Absolute),
                    $"_apis/git/repositories/{query.RepositoryName}/commits/{query.LastCommitId}").ToString(), queryParameters);
                var response = await httpClient.GetAsync(repositoryUrl);
                var responseContent = await response.Content.ReadAsStringAsync();
                var commitResponse = JsonConvert.DeserializeObject<Commit>(responseContent);
                return commitResponse.Committer.Date;
            }
        }
    }
}