using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReleaseNotesGenerator.Domain.WorkItem
{
    public class WorkItem
    {        
        public int Id { get; set; }

        [JsonProperty("fields")]
        public Field Field { get; set; }
    }

    public class Field
    {
        [JsonProperty("System.Title")]
        public string Title { get; set; }
    }
}