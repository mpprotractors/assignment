using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mp.Protractors.Entities.Models
{
    public class DependencyItem
    {
        public char Name { get; set; }
        public string RawDependencyList { get; set; }
        public string ParsedDependencyList { get; set; }

        public override string ToString()
        {
            return Name + "->" + (string.IsNullOrWhiteSpace(ParsedDependencyList) ? RawDependencyList : ParsedDependencyList);
        }
    }
}
