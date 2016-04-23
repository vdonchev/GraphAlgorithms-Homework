namespace _05.BreakCycles
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class BreakCycles
    {
        private static Dictionary<string, List<string>> childNodes;
        private static List<Edge> edges;
        private static List<Edge> edgesToRemove;
        private static HashSet<string> visited;
        private static bool isCyclic;

        public static void Main()
        {
            edges = new List<Edge>();
            childNodes = new Dictionary<string, List<string>>();
            edgesToRemove = new List<Edge>();

            GetInput();
            edges.Sort();
            RemoveCyclicEdges();
            Print();
        }

        private static void RemoveCyclicEdges()
        {
            foreach (Edge edge in edges)
            {
                if (!childNodes[edge.Start].Contains(edge.End) || !childNodes[edge.End].Contains(edge.Start))
                {
                    continue;
                }

                childNodes[edge.Start].Remove(edge.End);
                childNodes[edge.End].Remove(edge.Start);

                visited = new HashSet<string>();
                isCyclic = false;
                TraverseGraph(edge.Start, edge.End);

                if (isCyclic)
                {
                    edgesToRemove.Add(edge);
                }
                else
                {
                    childNodes[edge.Start].Add(edge.End);
                    childNodes[edge.End].Add(edge.Start);
                }
            }
        }

        private static void Print()
        {
            Console.WriteLine("Edges to remove: {0}", edgesToRemove.Count);
            edgesToRemove.ForEach(Console.WriteLine);
        }

        private static void TraverseGraph(string startNode, string endNode)
        {
            if (!visited.Contains(startNode))
            {
                if (startNode == endNode)
                {
                    isCyclic = true;
                }

                visited.Add(startNode);

                for (int i = 0; i < childNodes[startNode].Count; i++)
                {
                    TraverseGraph(childNodes[startNode][i], endNode);
                }
            }
        }

        private static void GetInput()
        {
            string nodeEdges = Console.ReadLine();

            while (nodeEdges.ToLower() != "end")
            {
                Regex regex = new Regex(@"(.{1}) -> (.+)");
                Match match = regex.Match(nodeEdges);

                string nodeStart = match.Groups[1].ToString();
                string[] nodeEnds = match.Groups[2]
                    .ToString()
                    .Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                childNodes.Add(nodeStart, new List<string>());

                foreach (var nodeEnd in nodeEnds)
                {
                    edges.Add(new Edge() { Start = nodeStart, End = nodeEnd });
                    childNodes[nodeStart].Add(nodeEnd);
                }

                nodeEdges = Console.ReadLine();
            }
        }
    }
}