using System;
using System.Collections.Generic;
using ReleaseNotes.Generator.Enums;

namespace ReleaseNotes.Generator.Components.Implementations
{
    public class ProjectTrackingToolFactory<T>
    {
        private ProjectTrackingToolFactory() { }

        private static readonly Dictionary<ProjectTrackingToolType, Func<T>> Implementations = new Dictionary<ProjectTrackingToolType, Func<T>>();

        public static T Create(ProjectTrackingToolType type)
        {
            Func<T> constructor;
            if (Implementations.TryGetValue(type, out constructor))
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