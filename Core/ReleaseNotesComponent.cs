using System;
using System.Collections.Generic;
using ReleaseNotesGenerator.Common.Models;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Domain.Commit;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ReleaesNotesGenerator.Common.Exceptions;
using ReleaseNotesGenerator.Core.ProjectTrackingToolHandlers;
using ReleaseNotesGenerator.Core.RepositoryHandlers;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Core
{
    public class ReleaseNotesComponent : IReleaseNotesComponent
    {
        private readonly Regex _regex = new Regex(@"(?<=#)\w+", RegexOptions.Compiled);
        private readonly ReleaseNotesContext _context;

        public ReleaseNotesComponent(ReleaseNotesContext context)
        {
            _context = context;
        }

        public async Task<string> Get(ReleaseNotesRequest releaseNotes)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var repository = await _context.Repositories.FindAsync(releaseNotes.RepositoryId);

                    var commits = await GetCommits(repository, releaseNotes.BranchName);
                    var workItemIds = GetWorkItemsIdsFromCommits(commits).ToList();
                    if (!workItemIds.Any())
                    {
                        throw new RelatedWorkItemsNotFoundException();
                    }

                    var projectTrackingTool = await _context.ProjectTrackingTools.FindAsync(repository.ProjectTrackingToolId);
                    var projectTrackingToolHandler =
                        ProjectTrackingToolFactory<IProjectTrackingToolHandler>.Create(projectTrackingTool.Type);
                    var workItems = await projectTrackingToolHandler.GetWorkItems(projectTrackingTool, workItemIds);

                    var linksToWorkItems = projectTrackingToolHandler.CreateLinksToWorkItems(projectTrackingTool, workItems);

                    transaction.Commit();
                    return linksToWorkItems;
                }
                catch (Exception e)
                {
                    // TODO: Log application to file or ApplicationInsights.
                    Console.WriteLine(e);
                    throw;
                }                
            }
        }

        private async Task<IList<Commit>> GetCommits(Repository repository, string branchName)
        {
            await _context.Entry(repository).Collection(r => r.Branches).LoadAsync();
            await _context.Entry(repository).Reference(r => r.RepositoryType).LoadAsync();

            var branch = repository.Branches.First(b => b.Name == branchName);
            var repositoryHandler = RepositoryFactory<IRepositoryHandler>.Create(repository.RepositoryType.Type);

            // TODO: Create AutoMapper mapping.
            var commits = await repositoryHandler.GetCommits(new CommitQuery()
            {
                Url = repository.Url,
                BranchName = branch.Name,
                DateTime = branch.LastCommitDateTime,
                RepositoryName = repository.Name,
                AccessToken = repository.AccessToken
            });

            if (!commits.Any())
            {
                // TODO: Map exception to HTTP response code and create global filter that will handle custom exception and return proper response code.
                throw new CommitsNotFoundException();
            }

            await SaveLastCommit(commits, branch);
            return commits;
        }

        private async Task SaveLastCommit(IEnumerable<Commit> commits, Branch branch)
        {
            var lastCommit = commits.OrderByDescending(commit => commit.Committer.Date).FirstOrDefault();

            branch.LastCommitDateTime = lastCommit.Committer.Date;
            branch.LastCommitId = lastCommit.CommitId;

            await _context.SaveChangesAsync();
        }

        private IEnumerable<string> GetWorkItemsIdsFromCommits(IEnumerable<Commit> commits)
        {
            var relatedWorkItems =
                commits.Select(commit => _regex.Match(commit.Comment).Value)
                    .Where(relatedWorkItem => !string.IsNullOrEmpty(relatedWorkItem));
            return relatedWorkItems;
        }
    }
}
