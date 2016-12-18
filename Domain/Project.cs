﻿using System.Collections.Generic;

namespace ReleaseNotesGenerator.Domain
{
    public class Project : EntityBase<int>
    {
        public string Name { get; set; }

        public string ApiKey { get; set; }

        public bool IsDeactivated { get; set; }

        public List<Repository> Repositories { get; set; }
    }
}
