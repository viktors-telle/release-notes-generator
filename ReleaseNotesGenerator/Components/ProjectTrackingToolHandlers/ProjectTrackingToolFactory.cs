using System;
using System.Collections.Generic;
using ReleaseNotesGenerator.Enums;

namespace ReleaseNotesGenerator.Components.ProjectTrackingToolHandlers
{
    public class ProjectTrackingToolFactory<T>
    {
        private ProjectTrackingToolFactory() { }

        private static readonly Dictionary<ProjectTrackingToolType, Func<T>> Implementations = new Dictionary<ProjectTrackingToolType, Func<T>>();

        public static T Create(ProjectTrackingToolType type)
        {
            if (Implementations.TryGetValue(type, out Func<T> constructor))
            {
                return constructor();
            }

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register(ProjectTrackingToolType type, Func<T> ctor)
        {
            if (Implementations.ContainsKey(type))
            {
                return;
            }

            Implementations.Add(type, ctor);
        }
    }
}