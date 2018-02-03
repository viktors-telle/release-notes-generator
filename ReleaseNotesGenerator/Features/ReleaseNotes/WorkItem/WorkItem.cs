using Newtonsoft.Json;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.WorkItem
{
    public class WorkItem
    {
        public int Id { get; set; }

        [JsonProperty("fields")]
        public Field Field { get; set; }
    }
}
