using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mp.Protractors.Test.Core.DependencyResolution
{
    public class Descriptor
    {
        public Descriptor(char item, IEnumerable<char> dependencies)
        {
            Item = item;
            Dependencies = dependencies.ToList();
        }

        public char Item { get; }

        public IReadOnlyList<char> Dependencies { get; }

        /// <summary>
        /// Parses string in expression "A -> BC" to Descriptor object.
        /// </summary>
        public static Descriptor Parse(string value)
        {
            var parts = value.Split("->");

            if (parts.Length != 2)
            {
                throw new ArgumentException($"Value {value} must be in the following expression: \"A -> BC\"");
            }

            var item = parts[0].Trim();

            if (item.Length > 1)
            {
                throw new ArgumentException("Item should be a char");
            }

            var dependenciesString = parts[1].Trim();
            var dependencies = dependenciesString.Select(c => c);

            return new Descriptor(char.Parse(item), dependencies);
        }
    }
}
