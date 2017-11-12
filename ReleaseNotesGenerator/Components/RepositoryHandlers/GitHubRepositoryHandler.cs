using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ReleaseNotesGenerator.Domain.Commit;

namespace ReleaseNotesGenerator.Components.RepositoryHandlers
{
    public class GitHubRepositoryHandler : IRepositoryHandler
    {
        private readonly HttpClientFactory _httpClientFactory;

        public GitHubRepositoryHandler(HttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Commit>> GetCommits(CommitQuery query)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "sha", query.BranchName }
            };

            if (query.From != default(DateTime))
            {
                queryParameters.Add("since", query.From.AddSeconds(1).ToString("s") + "Z");
            }

            if (query.Until != default(DateTime))
            {
                queryParameters.Add("until", query.Until.ToString("s") + "Z");
            }

            var client = _httpClientFactory.Create(new Uri(query.Url, UriKind.Absolute));
            var commitsUrl = QueryHelpers.AddQueryString(
                new Uri(client.BaseAddress, $"repos/{query.Owner}/{query.RepositoryName}/commits").ToString(), queryParameters);            
            var response = await client.GetAsync(commitsUrl);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IList<Commit>>(responseContent);
        }

        public async Task<IEnumerable<Commit>> GetCommitsWithFullComments(CommitQuery query, IEnumerable<Commit> commits)
        {
            return await Task.FromResult(new List<Commit>());
        }
    }
}