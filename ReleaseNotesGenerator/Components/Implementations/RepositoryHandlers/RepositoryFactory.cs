using System;
using System.Collections.Generic;
using ReleaseNotesGenerator.Enums;

namespace ReleaseNotesGenerator.Components.Implementations.RepositoryHandlers
{
    public class RepositoryFactory<T>
    {
        private RepositoryFactory() { }

        private static readonly Dictionary<RepositoryType, Func<T>> Implementations = new Dictionary<RepositoryType, Func<T>>();

        public static T Create(RepositoryType repositoryType)
        {
            if (Implementations.TryGetValue(repositoryType, out Func<T> constructor))
            {
                return constructor();
            }

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register(RepositoryType repositoryType, Func<T> ctor)
        {
            if (Implementations.ContainsKey(repositoryType))
            {
                return;
            }

            Implementations.Add(repositoryType, ctor);
        }
    }
}