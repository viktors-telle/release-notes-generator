namespace ReleaseNotesGenerator.Domain
{
    public class Project : EntityBase<int>
    {
        public string Name { get; set; }

        public string ApiKey { get; set; }
    }
}
