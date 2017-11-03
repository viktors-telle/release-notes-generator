using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReleaseNotesGenerator.Domain
{
    public class Project : EntityBase<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ApiKey { get; set; }

        public bool IsDeactivated { get; set; }

        public List<Repository> Repositories { get; set; }
    }
}
