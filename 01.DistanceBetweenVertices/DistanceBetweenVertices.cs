namespace _01.DistanceBetweenVertices
{
    using System;
    using System.Collections.Generic;

    public static class DistanceBetweenVertices
    {
        private static Dictionary<int, List<int>> graph;

        public static void Main()
        {
            graph = new Dictionary<int, List<int>>()
            {
                { 1, new List<int>() { 4 } },
                { 2, new List<int>() { 4 } },
                { 3, new List<int>() { 4, 5 } },
                { 4, new List<int>() { 6 } },
                { 5, new List<int>() { 3, 7, 8 } },
                { 6, new List<int>() },
                { 7, new List<int>() { 8 } },
                { 8, new List<int>() }
            };

            var distancesToFind = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1, 6),
                new Tuple<int, int>(1, 5),
                new Tuple<int, int>(5, 6),
                new Tuple<int, int>(5, 8),
            };

            foreach (var verticesPair in distancesToFind)
            {
                var shortestDistance = FindShortestDistance(verticesPair.Item1, verticesPair.Item2);
                Console.WriteLine($"{{{verticesPair.Item1}, {verticesPair.Item2}}} -> {shortestDistance}");
            }
        }

        private static int FindShortestDistance(int startVertex, int endVertex)
        {
            var visited = new HashSet<int>();

            var queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(new Tuple<int, int>(startVertex, 0));

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                visited.Add(currentNode.Item1);

                if (currentNode.Item1 == endVertex)
                {
                    return currentNode.Item2;
                }

                foreach (var child in graph[currentNode.Item1])
                {
                    if (!visited.Contains(child))
                    {
                        queue.Enqueue(new Tuple<int, int>(child, currentNode.Item2 + 1));
                    }
                }
            }

            return -1;
        }
    }
}
