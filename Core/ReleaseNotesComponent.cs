using System.Collections.Generic;
using ReleaseNotesGenerator.Common.Models;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Domain.Commit;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ReleaesNotesGenerator.Common.Exceptions;
using ReleaseNotesGenerator.Core.RepositoryHandlers;

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
            var repository = await _context.Repositories.FindAsync(releaseNotes.RepositoryId);
            await _context.Entry(repository).Collection(r => r.Branches).LoadAsync();
            await _context.Entry(repository).Reference(r => r.RepositoryType).LoadAsync();

            var branch = repository.Branches.First(b => b.Name == releaseNotes.BranchName);
            var repositoryHandler = RepositoryFactory<IRepositoryHandler>.Create(repository.RepositoryType.Type);
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
                throw new CommitsNotFoundException();
            }

            var lastCommit = commits.OrderByDescending(commit => commit.Committer.Date).FirstOrDefault();

            branch.LastCommitDateTime = lastCommit.Committer.Date;
            branch.LastCommitId = lastCommit.CommitId;

            await _context.SaveChangesAsync();

            var workItems = GetWorkItemsIds(commits);

            // TODO: Query project tracking tool for work items.

            return string.Empty;
        }

        private IEnumerable<string> GetWorkItemsIds(IEnumerable<Commit> commits)
        {
            var relatedWorkItems =
                commits.Select(commit => _regex.Match(commit.Comment).Value)
                    .Where(relatedWorkItem => !string.IsNullOrEmpty(relatedWorkItem));
            return relatedWorkItems;
        }
    }
}
