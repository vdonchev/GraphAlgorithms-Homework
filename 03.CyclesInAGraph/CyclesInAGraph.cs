namespace _03.CyclesInAGraph
{
    using System;
    using System.Collections.Generic;

    public static class CyclesInAGraph
    {
        private static Dictionary<string, List<string>> graph;
        private static HashSet<string> visitedNodes = new HashSet<string>();

        public static void Main()
        {
            graph = new Dictionary<string, List<string>>();

            var input = Console.ReadLine();
            while (input != string.Empty)
            {
                var vertices = input.Split(new[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (!graph.ContainsKey(vertices[0]))
                {
                    graph[vertices[0]] = new List<string>();
                }

                if (!graph.ContainsKey(vertices[1]))
                {
                    graph[vertices[1]] = new List<string>();
                }

                graph[vertices[0]].Add(vertices[1]);
                graph[vertices[1]].Add(vertices[0]);

                input = Console.ReadLine();
            }

            var result = false;
            foreach (var vertex in graph.Keys)
            {
                result = CheckGraphForCycle(vertex);
                if (result)
                {
                    break;
                }
            }

            Console.WriteLine($"Acyclic: {(result ? "No" : "Yes")}");
        }

        private static bool CheckGraphForCycle(string vertex)
        {
            var queue = new Queue<Tuple<string, string>>(); // <node, node's parent>
            var cyclicNodes = new HashSet<string>();

            queue.Enqueue(new Tuple<string, string>(vertex, null));
            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();

                if (cyclicNodes.Contains(currentVertex.Item1))
                {
                    return true;
                }

                visitedNodes.Add(currentVertex.Item1);
                cyclicNodes.Add(currentVertex.Item1);
                
                foreach (var child in graph[currentVertex.Item1])
                {
                    if (child != currentVertex.Item2)
                    {
                        queue.Enqueue(new Tuple<string, string>(child, currentVertex.Item1));
                    }
                }
            }

            return false;
        }
    }
}
