using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_1._2_LatinSquare
{
    public static class LatinSquare_ForwardChecking
    {
        static int N = 5;
        static bool finished = false;
        static int[] candidateNumber;
        static int[,] grid = {
                   { 1,2,3,4,5,6,7,8,9 },
                   { 0,0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0,0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0,0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0,0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0,0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0,0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0,0, 0, 0, 0, 0, 0, 0, 0 },
                   { 0,0, 0, 0, 0, 0, 0, 0, 0 }
            };

        // Driver code
        public static void Main_LatinSquare_ForwardChecking()
        {

            if (solveLatinSquare(grid, 0, 0, Enumerable.Range(1, N).ToList()))
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
            candidateNumber = new int[N];
            candidateNumber = Enumerable.Range(1, N).ToArray();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    grid[i, j] = 0;
            var firstRow = Enumerable.Range(1, n).ToArray();
            for (int i = 0; i < firstRow.Length; i++)
                grid[0, i] = firstRow[i];
        }

        static bool solveLatinSquare(int[,] grid, int row, int col, List<int> candidates)
        {
            bool valid = false;
            if (row == N - 1 && col == N /*&& candidates.Count == 0*/)
            {
                finished = true;
                return true;
            }

            if (col == N)
            {
                row++; col = 0;
                candidates = candidateNumber.ToList();
            }


            if (grid[row, col] != 0)
                return solveLatinSquare(grid, row, col + 1, candidates);


            for (int num = 1; num < N + 1; num++)//select value
                if (forwardChecking(grid, row, col, /*num, */candidates))
                    valid = true;
            return valid;
        }

        public static bool forwardChecking(int[,] grid, int row, int col, /*int num,*/ List<int> candidateNumber)
        {
            bool isValid = false;
            var list = new Queue<int>();
            list.AddRange<int>(candidateNumber);
            while (list.Count > 0 && !finished)
            {
                var num = list.Dequeue();
                if (isSafe(grid, row, col, num))
                {
                    grid[row, col] = num;
                    if (solveLatinSquare(grid, row, col + 1, list.ToList()))//step forward
                        return true;
                }
                else
                    list.Enqueue(num);
            }
            if (list.Count > 0 && !finished)
                isValid = true;
            return isValid;
        }

        static void print(int[,] grid)
        {
            Console.WriteLine("Solution: \n");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    Console.Write(grid[i, j] + " ");
                Console.WriteLine();
            }
        }

        static bool isSafe(int[,] grid, int row, int col, int num)
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
