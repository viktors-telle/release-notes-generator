using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ReleaseNotesGenerator.Extensions
{
    public static class CommitListExtensions
    {
        private static readonly Regex Regex = new Regex(@"(?<=#)\d+", RegexOptions.Compiled);

        public static IEnumerable<string> GetDistinctWorkItemsIdsFromCommits(this IEnumerable<Domain.Commit.Commit> commits)
        {
            var relatedWorkItems = new List<string>();
            foreach (var commit in commits)
            {
                if (string.IsNullOrEmpty(commit.Comment))
                {
                    continue;                    
                }
                var matches = Regex.Matches(commit.Comment);
                foreach (Match match in matches)
                {
                    if (!relatedWorkItems.Contains(match.Value))
                    {
                        relatedWorkItems.Add(match.Value);
                    }
                }
            }

            return relatedWorkItems;
        }
    }
}