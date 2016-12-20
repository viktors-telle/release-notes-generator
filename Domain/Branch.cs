﻿using System;

namespace ReleaseNotesGenerator.Domain
{
    public class Branch : EntityBase<int>
    {
        public string Name { get; set; }

        public string LastCommitId { get; set; }

        public DateTime? LastCommitDateTime { get; set; }        

        public int RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}
