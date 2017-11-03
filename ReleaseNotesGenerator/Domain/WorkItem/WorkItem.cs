using Newtonsoft.Json;

namespace ReleaseNotesGenerator.Domain.WorkItem
{
    public class WorkItem
    {
        public int Id { get; set; }

        [JsonProperty("fields")]
        public Field Field { get; set; }
    }
}
