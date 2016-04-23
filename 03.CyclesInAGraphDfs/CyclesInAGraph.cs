namespace _03.CyclesInAGraphDfs
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class CyclesInAGraphBfs
    {
        private static Dictionary<string, List<string>> graph;
        private static HashSet<string> visitedNodes = new HashSet<string>();
        private static HashSet<string> ciclyNodes = new HashSet<string>();

        public static void Main()
        {
            graph = new Dictionary<string, List<string>>();

            var input = Console.ReadLine();
            while (input != string.Empty)
            {
                var nodes = Regex.Split(input, @"[^\w]+");
                if (!graph.ContainsKey(nodes[0]))
                {
                    graph[nodes[0]] = new List<string>();
                }

                if (!graph.ContainsKey(nodes[1]))
                {
                    graph[nodes[1]] = new List<string>();
                }

                graph[nodes[0]].Add(nodes[1]);
                graph[nodes[1]].Add(nodes[0]);

                input = Console.ReadLine();
            }

            var result = false;
            foreach (var node in graph.Keys)
            {
                CheckIfGraphIsCyclic(new Tuple<string, string>(node, null), ref result);
                if (result)
                {
                    break;
                }
            }

            Console.WriteLine($"Acyclic: {(result ? "No" : "Yes")}");
        }

        private static void CheckIfGraphIsCyclic(Tuple<string, string> node, ref bool result)
        {
            var nodeValue = node.Item1;
            var nodeParent = node.Item2;

            if (ciclyNodes.Contains(nodeValue))
            {
                result = true;
                return;
            }

            visitedNodes.Add(nodeValue);
            ciclyNodes.Add(nodeValue);

            foreach (var child in graph[nodeValue])
            {
                if (child != nodeParent)
                {
                    CheckIfGraphIsCyclic(new Tuple<string, string>(child, nodeValue), ref result);
                }
            }

            ciclyNodes.Remove(nodeValue);
        }
    }
}
