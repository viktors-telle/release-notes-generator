﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReleaseNotesGenerator.Dal;
using ReleaseNotesGenerator.Exceptions;
using ReleaseNotesGenerator.Extensions;
using ReleaseNotesGenerator.Features.ReleaseNotes.Commit;
using ReleaseNotesGenerator.Features.ReleaseNotes.ProjectTrackingToolHandlers;
using ReleaseNotesGenerator.Features.ReleaseNotes.RepositoryHandlers;
using ReleaseNotesGenerator.Features.SourceCodeRepositories;

namespace ReleaseNotesGenerator.Features.ReleaseNotes
{
    public class ReleaseNotesComponent : IReleaseNotesComponent
    {
        private readonly ReleaseNotesContext _context;
        private readonly IMapper _mapper;

        public ReleaseNotesComponent(ReleaseNotesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Get(ReleaseNotesRequest request)
        {
            var repository = await GetRepository(request);
            using (var transaction = _context.Database.BeginTransaction())
            {
                var commits = await GetCommits(repository, request);
                var releaseNotes = await GenerateReleaseNotes(commits, repository);
                await Save(releaseNotes, repository.Id, request);
                transaction.Commit();
                return releaseNotes;
            }
        }

        public async Task<List<ReleaseNote>> GetAll()
        {
            return await _context.ReleaseNotes.ToListAsync();
        }

        private async Task<Repository> GetRepository(ReleaseNotesRequest request)
        {
            var project =
                await _context.Projects.FirstOrDefaultAsync(p => p.Name == request.ProjectName && !p.IsDeactivated);
            if (project == null)
            {
                throw new ProjectNotExistsOrIsDeactivatedException();
            }

            await _context.Entry(project).Collection(r => r.Repositories).LoadAsync();
            var repository =
                project.Repositories.FirstOrDefault(
                    r => r.Name.Equals(request.RepositoryName, StringComparison.InvariantCultureIgnoreCase));
            if (repository == null)
            {
                throw new RepositoryNotFoundException();
            }                

            return repository;
        }

        private async Task<IList<Commit.Commit>> GetCommits(Repository repository, ReleaseNotesRequest request)
        {
            var commitQuery = _mapper.Map<Repository, CommitQuery>(repository);
            _mapper.Map(request, commitQuery);
            var repositoryHandler = RepositoryFactory<IRepositoryHandler>.Create(repository.RepositoryType);
            var commits = (await repositoryHandler.GetCommits(commitQuery)).ToList();

            if (!commits.Any())
            {
                throw new CommitsNotFoundException();
            }                

            commits.AddRange(await repositoryHandler.GetCommitsWithFullComments(commitQuery,
                commits.Where(c => c.CommentTruncated)));
            commits.RemoveAll(c => c.CommentTruncated);
            await _context.SaveChangesAsync();
            return commits;
        }

        private async Task<string> GenerateReleaseNotes(IList<Commit.Commit> commits, Repository repository)
        {                         
            var projectTrackingTool = repository.ProjectTrackingToolId != default(int?) 
                ? await _context.ProjectTrackingTools.FindAsync(repository.ProjectTrackingToolId) 
                : null;
            if (projectTrackingTool == null)
            {
                return commits.Select(c => c.Comment).Aggregate((a, b) => a + Environment.NewLine + b);
            }

            var workItemIds = commits.GetDistinctWorkItemsIdsFromCommits().ToArray();
            if (!workItemIds.Any())
            {
                throw new RelatedWorkItemsNotFoundException();
            }

            var projectTrackingToolHandler =
                ProjectTrackingToolFactory<IProjectTrackingToolHandler>.Create(projectTrackingTool.Type);

            var workItems = await projectTrackingToolHandler.GetWorkItems(projectTrackingTool, workItemIds);
            var linksToWorkItems = projectTrackingToolHandler.CreateLinksToWorkItems(projectTrackingTool, workItems);
            return linksToWorkItems;
        }       

        private async Task<int> Save(string releaseNotes, int repositoryId, ReleaseNotesRequest request)
        {
            var releaseNote = _mapper.Map<ReleaseNotesRequest, ReleaseNote>(request);
            releaseNote.RepositoryId = repositoryId;
            releaseNote.Created = DateTime.UtcNow;
            releaseNote.Notes = releaseNotes;

            _context.ReleaseNotes.Add(releaseNote);
            return await _context.SaveChangesAsync();
        }
    }
}