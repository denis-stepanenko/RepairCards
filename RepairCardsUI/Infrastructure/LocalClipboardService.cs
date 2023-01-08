using AgileObjects.AgileMapper.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace RepairCardsUI.Infrastructure
{
    public static class LocalClipboardService
    {
        private static List<object> s_objects = new List<object>();

        public static void Set(List<object> objects) => s_objects = objects;

        public static void Set<T>(List<T> objects)
        {
            s_objects = objects.Cast<object>().ToList();
        }

        public static List<T> Get<T>()
        {
            var clonedObjects = s_objects.DeepClone().OfType<T>().ToList();
            if (clonedObjects.Count > 0) s_objects.Clear();
            return clonedObjects;
        }
    }
}
