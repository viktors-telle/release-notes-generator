using System.ComponentModel.DataAnnotations;

namespace ReleaseNotes.Generator.Domain
{
    public class RepositoryItemPath : EntityBase<int>
    {
        [Required]
        public string Path { get; set; }

        [Required]
        public int BranchId { get; set; }

        public string LastCommitId { get; set; }

        public Branch Branch{ get; set; }
    }
}
