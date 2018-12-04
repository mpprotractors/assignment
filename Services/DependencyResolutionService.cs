using System;
using System.Collections.Generic;
using System.Linq;
using Mp.Protractors.Test.Models;

namespace Mp.Protractors.Test.Services
{
    public class DependencyResolutionService : IDependencyResolutionService
    {
        public DependencyResolutionService()
        {
            Init();
        }

        private void Init()
        {
            var nodeA = new NodeModel("A");
            nodeA.AddEdge("B");
            nodeA.AddEdge("C");
            var nodeB = new NodeModel("B");
            nodeB.AddEdge("C");
            nodeB.AddEdge("E");
            var nodeC = new NodeModel("C");
            nodeC.AddEdge("G");
            var nodeD = new NodeModel("D");
            nodeD.AddEdge("A");
            nodeD.AddEdge("F");
            var nodeE = new NodeModel("E");
            nodeE.AddEdge("F");
            var nodeF = new NodeModel("F");
            nodeF.AddEdge("H");
            var nodeG = new NodeModel("G");
            var nodeH = new NodeModel("H");

            Nodes = new List<NodeModel>
            {
                nodeA,
                nodeB,
                nodeC,
                nodeD,
                nodeE,
                nodeF,
                nodeG,
                nodeH
            };
        }

        public IList<NodeModel> Nodes { get; set; }

        public IEnumerable<string> ResolveDependencies(string nodeName)
        {
            var resolvedNodes = new List<NodeModel>();
            var seenNodes = new List<NodeModel>();
            var node = Nodes.FirstOrDefault(n => n.Node.ToLower() == nodeName.ToLower());
            Resolve(node, resolvedNodes, seenNodes, true);
            return resolvedNodes.Select(n => n.Node).OrderBy(n => n);
        }

        private void Resolve(NodeModel node, List<NodeModel> resolvedNodes, List<NodeModel> seenNodes, bool skipFirstNode = false)
        {
            seenNodes.Add(node);
            foreach (var nodeEdge in node.Edges)
            {
                if (resolvedNodes.All(rn => rn.Node != nodeEdge))
                {
                    if (seenNodes.Any(rn => rn.Node == nodeEdge))
                    {
                        throw new StackOverflowException($"{node.Node} {nodeEdge}");
                    }
                    var nextNode = Nodes.FirstOrDefault(n => n.Node.ToLower() == nodeEdge.ToLower());
                    Resolve(nextNode, resolvedNodes, seenNodes);
                }
            }

            if (!skipFirstNode)
            {
                resolvedNodes.Add(node);
            }
        }
    }
}