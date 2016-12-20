﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Core.ProjectTrackingToolHandlers
{
    public interface IProjectTrackingToolHandler
    {
        Task<WorkItem> GetWorkItems(IEnumerable<string> workItemIds);
    }
}