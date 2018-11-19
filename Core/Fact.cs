using System.Collections.Generic;

namespace Mp.Protractors.Test.Core
{
    public class Fact
    {
        public bool Solved { get; set; }
        public string Item { get; set; }
        public bool IsFact { get; set; }
        public List<Fact> Dependencies { get; set; }
        public List<Fact> Solution { get; set; }

        public Fact (string item, bool isFact)
        {
            Item = item;
            Solved = false;
            Solution = new List<Fact>();
            Dependencies = new List<Fact>();
            IsFact = isFact;
            Solved = !isFact;

            if (!IsFact) {
                Solution.Add(this);
            }
        }
    }
}