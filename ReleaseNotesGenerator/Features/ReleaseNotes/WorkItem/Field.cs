﻿using Newtonsoft.Json;

namespace ReleaseNotesGenerator.Features.ReleaseNotes.WorkItem
{
    public class Field
    {
        [JsonProperty("System.Title")]
        public string Title { get; set; }

        [JsonProperty("System.State")]
        public string State { get; set; }
    }
}