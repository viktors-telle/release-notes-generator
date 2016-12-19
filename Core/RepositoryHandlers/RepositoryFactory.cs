using ReleaesNotesGenerator.Common.Enums;
using System;
using System.Collections.Generic;

namespace ReleaseNotesGenerator.RepositoryHandlers.Core
{
    public class RepositoryFactory<T>
    {
        private RepositoryFactory() { }

        private static readonly Dictionary<RepositoryType, Func<T>> _implementations = new Dictionary<RepositoryType, Func<T>>();

        public static T Create(RepositoryType repositoryType)
        {
            Func<T> constructor = null;
            if (_implementations.TryGetValue(repositoryType, out constructor))
            {
                return constructor();
            }                

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register(RepositoryType repositoryType, Func<T> ctor)
        {
            if (_implementations.ContainsKey(repositoryType))
            {
                return;
            }

            _implementations.Add(repositoryType, ctor);
        }
    }
}
