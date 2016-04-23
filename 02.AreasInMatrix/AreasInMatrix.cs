namespace _02.AreasInMatrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class AreasInMatrix
    {
        private static char[][] matrix;
        private static bool[,] visited;
        private static SortedDictionary<char, int> areas = 
            new SortedDictionary<char, int>(); 

        public static void Main()
        {
            var rows = int.Parse(Console.ReadLine().Last().ToString());
            matrix = new char[rows][];
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = Console.ReadLine().ToCharArray();
            }

            var cols = matrix[0].Length;

            visited = new bool[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (!visited[row, col])
                    {
                        if (!areas.ContainsKey(matrix[row][col]))
                        {
                            areas[matrix[row][col]] = 0;
                        }

                        Dfs(row, col, matrix[row][col]);

                        areas[matrix[row][col]]++;
                    }
                }
            }

            Console.WriteLine($"Areas: {areas.Values.Sum()}");
            foreach (var area in areas)
            {
                Console.WriteLine($"Letter '{area.Key}' -> {area.Value}");
            }
        }

        private static void Dfs(int row, int col, char currentChar)
        {
            if (row < 0 || row >= matrix.Length ||
                col < 0 || col >= matrix[0].Length ||
                visited[row, col] ||
                matrix[row][col] != currentChar)
            {
                return;
            }

            visited[row, col] = true;

            Dfs(row, col + 1, currentChar);
            Dfs(row + 1, col, currentChar);
            Dfs(row, col - 1, currentChar);
            Dfs(row - 1, col, currentChar);
        }
    }
}
