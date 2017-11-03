using System;
using System.Collections.Generic;
using ReleaseNotes.Generator.Enums;

namespace ReleaseNotes.Generator.Components.Implementations
{
    public class RepositoryFactory<T>
    {
        private RepositoryFactory() { }

        private static readonly Dictionary<RepositoryType, Func<T>> Implementations = new Dictionary<RepositoryType, Func<T>>();

        public static T Create(RepositoryType repositoryType)
        {
            Func<T> constructor;
            if (Implementations.TryGetValue(repositoryType, out constructor))
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