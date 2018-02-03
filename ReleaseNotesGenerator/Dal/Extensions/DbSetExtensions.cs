using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReleaseNotesGenerator.Dal.Extensions
{
    public static class DbSetExtensions
    {
        public static void AddOrUpdate<T>(this DbSet<T> set, Func<T, object> matcher, T item) where T : EntityBase<int>
        {
            var itemInSet = set.AsNoTracking().FirstOrDefault(e => matcher(e).Equals(matcher(item)));
            if (itemInSet == null)
            {
                set.Add(item);
                return;
            }
            item.Id = itemInSet.Id;
            set.Update(item);
            
        }

        public static void AddOrUpdate<T>(this DbSet<T> set, Func<T, object> matcher, IEnumerable<T> items) where T : EntityBase<int>
        {
            foreach (var item in items)
            {
                set.AddOrUpdate(matcher, item);
            }
        }
    }
}
