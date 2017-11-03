﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ReleaseNotes.Generator.Dal;
using ReleaseNotes.Generator.Domain;
using ReleaseNotes.Generator.Domain.Commit;
using ReleaseNotes.Generator.Dto;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReleaseNotes.Generator.Components.Interfaces;
using ReleaseNotes.Generator.Exceptions;

namespace ReleaseNotes.Generator.Components.Implementations
{
    public class ReleaseNotesComponent : IReleaseNotesComponent
    {
        private readonly Regex _regex = new Regex(@"(?<=#)\d+", RegexOptions.Compiled);
        private readonly ReleaseNotesContext _context;
        private readonly IMapper _mapper;

        public ReleaseNotesComponent(ReleaseNotesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Get(ReleaseNotesRequest releaseNotes)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Name == releaseNotes.ProjectName && !p.IsDeactivated);
            if (project == null)
            {
                throw new ProjectIsDeactivatedException();
            }

            await _context.Entry(project).Collection(r => r.Repositories).LoadAsync();
            var repository = project.Repositories.FirstOrDefault(r => r.Name.Equals(releaseNotes.RepositoryName, StringComparison.InvariantCultureIgnoreCase));
            if (repository == null)
            {
                throw new RepositoryNotFoundException();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                var commits = await GetCommits(repository, releaseNotes.BranchName, releaseNotes.RepositoryItemPath);
                var workItemIds = GetDistinctWorkItemsIdsFromCommits(commits).ToArray();
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
        }

        private async Task<IList<Commit>> GetCommits(Repository repository, string branchName, string repositoryItemPath)
        {
            await _context.Entry(repository).Collection(r => r.Branches).LoadAsync();            
            var branch = repository.Branches.First(b => b.Name == branchName);
            await _context.Entry(branch).Collection(r => r.RepositoryItemPaths).LoadAsync();
            var repositoryHandler = RepositoryFactory<IRepositoryHandler>.Create(repository.RepositoryType);

            var commitQuery = _mapper.Map<Repository, CommitQuery>(repository);
            _mapper.Map(branch, commitQuery);

            RepositoryItemPath itemPath = null;
            if (!string.IsNullOrEmpty(repositoryItemPath))
            {
                itemPath = branch.RepositoryItemPaths.FirstOrDefault(r => r.Path.Equals(repositoryItemPath, StringComparison.InvariantCultureIgnoreCase));
                if (itemPath == null)
                {
                    throw new RepositoryItemPathDoesNotExistException();
                }

                commitQuery.ItemPath = itemPath.Path;
                commitQuery.LastCommitId = itemPath.LastCommitId;
            }
            
            var commits = (await repositoryHandler.GetCommits(commitQuery)).ToList();

            if (!commits.Any())
            {
                throw new CommitsNotFoundException();
            }

            commits.AddRange(await repositoryHandler.GetCommitsWithFullComments(commitQuery, commits.Where(c => c.CommentTruncated)));
            commits.RemoveAll(c => c.CommentTruncated);

            if (itemPath != null)
            {
                await SaveLastItemPathCommit(commits, itemPath);
            }
            else
            {
                await SaveLastBranchCommit(commits, branch);
            }
            
            return commits;
        }

        private async Task SaveLastBranchCommit(IEnumerable<Commit> commits, Branch branch)
        {
            var lastCommit = commits.OrderByDescending(commit => commit.Committer.Date).FirstOrDefault();

            branch.LastCommitDateTime = lastCommit.Committer.Date;
            branch.LastCommitId = lastCommit.CommitId;

            await _context.SaveChangesAsync();
        }

        private async Task SaveLastItemPathCommit(IEnumerable<Commit> commits, RepositoryItemPath itemPath)
        {
            var lastCommit = commits.OrderByDescending(commit => commit.Committer.Date).FirstOrDefault();            
            itemPath.LastCommitId = lastCommit.CommitId;
            await _context.SaveChangesAsync();
        }

        private IEnumerable<string> GetDistinctWorkItemsIdsFromCommits(IEnumerable<Commit> commits)
        {
            var relatedWorkItems = new List<string>();
            foreach (var commit in commits)
            {
                var matches = _regex.Matches(commit.Comment);
                foreach (Match match in matches)
                {
                    if (!relatedWorkItems.Contains(match.Value))
                    {
                        relatedWorkItems.Add(match.Value);
                    }
                }
            }

            return relatedWorkItems;
        }
    }
}