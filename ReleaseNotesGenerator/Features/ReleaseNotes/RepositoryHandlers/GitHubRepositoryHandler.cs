using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ReleaseNotesGenerator.Features.ReleaseNotes.Commit;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.RepositoryHandlers
{
    public class GitHubRepositoryHandler : IRepositoryHandler
    {
        private readonly HttpClientFactory _httpClientFactory;

        public GitHubRepositoryHandler(HttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Commit.Commit>> GetCommits(CommitQuery query)
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
            client.DefaultRequestHeaders.Add("User-Agent", query.RepositoryName);
            var commitsUrl = QueryHelpers.AddQueryString(
                new Uri(client.BaseAddress, $"repos/{query.Owner}/{query.RepositoryName}/commits").ToString(), queryParameters);            
            var response = await client.GetAsync(commitsUrl);
            if (!response.IsSuccessStatusCode)
            {
                return new List<Commit.Commit>();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var commitResponses = JsonConvert.DeserializeObject<List<GitHubCommitResponse>>(responseContent);
            return commitResponses.Select(cr => cr.Commit).ToList();
        }

        public async Task<IEnumerable<Commit.Commit>> GetCommitsWithFullComments(CommitQuery query, IEnumerable<Commit.Commit> commits)
        {
            return await Task.FromResult(new List<Commit.Commit>());
        }
    }
}