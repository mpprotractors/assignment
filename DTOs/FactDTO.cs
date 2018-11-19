using System.Collections.Generic;

namespace Mp.Protractors.Test.DTOs
{
    public class FactDTO
    {
        public string Item { get; set; }
        public List<string> Dependencies { get; set; }

        public FactDTO (string item, List<string> dependencies) 
        {
            Item = item;
            Dependencies = dependencies;
        }
    }
}