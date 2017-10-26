using System;

namespace ReleaseNotes.Generator.Domain.Commit
{
    public class Author
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }
    }
}