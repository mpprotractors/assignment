using System;
using System.Collections.Generic;
using System.Linq;

namespace Mp.Protractors.Test.Core.DependencyResolution
{
    public class DependencyContainer : IDependencyContainer
    {
        private readonly Dictionary<object, IEnumerable<object>> _descriptors;

        public DependencyContainer()
        {
            _descriptors = new Dictionary<object, IEnumerable<object>>();
        }

        public void Register(object item, params object[] dependencies)
        {
            if (_descriptors.ContainsKey(item))
            {
                throw new InvalidOperationException("Such item was already registered");
            }

            _descriptors.Add(item, dependencies);
        }

        public IEnumerable<object> ResolveAllDependencies(object item)
        {
            if (!_descriptors.ContainsKey(item))
            {
                throw new InvalidOperationException("Such item was not registered");
            }

            return ResolveAllDependenciesInternal(item);
        }

        private IEnumerable<object> ResolveAllDependenciesInternal(object item)
        {
            var visitedItems = new HashSet<object>();
            var stack = new Stack<object>();

            visitedItems.Add(item);
            stack.Push(item);

            while (stack.Any())
            {
                var currentItem = stack.Pop();
                var dependencies = ResolveDependencies(currentItem);

                foreach (var dependency in dependencies)
                {
                    if (!visitedItems.Contains(dependency))
                    {
                        visitedItems.Add(dependency);
                        stack.Push(dependency);
                        yield return dependency;
                    }
                }
            }
        }

        private IEnumerable<object> ResolveDependencies(object item)
        {
            if (!_descriptors.ContainsKey(item))
            {
                return new object[0];
            }

            return _descriptors[item];
        }
    }
}
