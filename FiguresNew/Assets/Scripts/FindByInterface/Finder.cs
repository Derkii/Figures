using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
    public static class Finder
    {
        private static Dictionary<Type[], Component> _allComponentsOnScene;

        public static IEnumerable<Component> FindByInterfaces(params Type[] types)
        {
            return _allComponentsOnScene.Where(t => t.Key.ContainsAll(types)).Select(t => t.Value);
        }

        private static bool ContainsAll<TType>(this IEnumerable<TType> ienumerable, IEnumerable<TType> all)
        {
            int count = 0;
            foreach (var element in all)
            {
                if (ienumerable.Contains(element))
                {
                    count++;
                }
            }
            if (count == all.Count()) return true;
            return false;
        }

        static Finder()
        {
            if (_allComponentsOnScene == null) _allComponentsOnScene = new Dictionary<Type[], Component>();
        }
        public static void AddComponent(Component component)
        {
            _allComponentsOnScene.Add(component.GetType().GetInterfaces(), component);
        }
    }
}