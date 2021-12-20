using System;
using System.Linq;

namespace Assignment_1._2_LatinSquare
{
    public static class LatinSquare_BackTracking
    {
        static int N = 5;
        static int[,] grid;

        // Driver code
        public static void Main_LatinSquare_BackTracking()
        {

            if (solveLatinSquare(grid, 0, 0))
                print(grid);
            else
                Console.WriteLine("No Solution exists");
        }

        public static void SetLatinSquareGrid(int n)
        {
            if (n <= 1)
                n = 4;
            N = n;
            grid = new int[N, N];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    grid[i, j] = 0;
            var firstRow = Enumerable.Range(1, n).ToArray();
            for (int i = 0; i < firstRow.Length; i++)
                grid[0, i] = firstRow[i];
        }

        static bool solveLatinSquare(int[,] grid, int row, int col)
        {
            if (row == N - 1 && col == N)
                return true;

            if (col == N)
            {
                row++; col = 0;
            }

            if (grid[row, col] != 0)
                return solveLatinSquare(grid, row, col + 1);

            for (int num = 1; num < N + 1; num++)
            {
                if (isSafe(grid, row, col, num))
                {
                    grid[row, col] = num;
                    if (solveLatinSquare(grid, row, col + 1))
                        return true;
                }
                grid[row, col] = 0;
            }
            return false;
        }

        /* A utility function to print grid */
        static void print(int[,] grid)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    Console.Write(grid[i, j] + " ");
                Console.WriteLine();
            }
        }

        static bool isSafe(int[,] grid, int row, int col,
                           int num)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (grid[row, i] == num)
                    return false;
            }
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (grid[i, col] == num)
                    return false;
            }

            return true;
        }
    }
}
