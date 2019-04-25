using System;
using System.Collections.Generic;
using System.Linq;

namespace Mp.Protractors.Test.Core.DependencyResolution
{
    public class CharDependencyContainer : ICharDependencyContainer
    {
        private readonly Dictionary<char, Descriptor> _descriptorsLookup;

        public CharDependencyContainer()
        {
            _descriptorsLookup = new Dictionary<char, Descriptor>();
        }

        public IReadOnlyList<Descriptor> GetDescriptors()
        {
            return _descriptorsLookup.Values.ToList();
        }

        public IReadOnlyList<Descriptor> GetDescriptorsIncludingTransitiveDependencies()
        {
            var descriptors = _descriptorsLookup.Keys.Select(k =>
            {
                var allDeps = ResolveAllDependencies(k);
                return new Descriptor(k, allDeps);
            }).ToList();

            return descriptors;
        }

        public void Register(char item, params char[] dependencies)
        {
            Register(new Descriptor(item, dependencies));
        }

        public void Register(Descriptor descriptor)
        {
            if (_descriptorsLookup.ContainsKey(descriptor.Item))
            {
                throw new InvalidOperationException("Such item was already registered");
            }

            _descriptorsLookup.Add(descriptor.Item, descriptor);
        }

        private IEnumerable<char> ResolveAllDependencies(char item)
        {
            var visitedItems = new HashSet<char>();
            var stack = new Stack<char>();

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

        private IEnumerable<char> ResolveDependencies(char item)
        {
            if (!_descriptorsLookup.ContainsKey(item))
            {
                return new char[0];
            }

            return _descriptorsLookup[item].Dependencies;
        }
    }
}
