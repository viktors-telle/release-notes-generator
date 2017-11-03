using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReleaseNotesGenerator.Domain
{
    public class Branch : EntityBase<int>
    {
        [Required]
        public string Name { get; set; }

        public string LastCommitId { get; set; }

        public DateTime? LastCommitDateTime { get; set; }

        [Required]
        public int RepositoryId { get; set; }

        public Repository Repository { get; set; }

        public List<RepositoryItemPath> RepositoryItemPaths { get; set; }
    }
}