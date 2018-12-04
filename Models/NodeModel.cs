using System;
using System.Collections.Generic;

namespace Mp.Protractors.Test.Models
{
    public class NodeModel
    {
        public NodeModel(string node)
        {
            Node = node;
        }

        public string Node { get; set; }
        public IList<string> Edges { get; set; } = new List<string>();

        public void AddEdge(string edge)
        {
            if (!string.IsNullOrEmpty(edge))
            {
                Edges.Add(edge);
                return;
            }

            throw new ArgumentException("Edge can not be null or empty!");
        }
    }
}