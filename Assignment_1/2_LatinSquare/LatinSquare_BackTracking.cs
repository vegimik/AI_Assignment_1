using System;
using System.Linq;

namespace Assignment_1._2_LatinSquare
{
    public static class LatinSquare_BackTracking
    {

        // N is the size of the 2D matrix   N*N
        static int N = 5;

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

        /* Takes a partially filled-in grid and attempts
          to assign values to all unassigned locations in
          such a way to meet the requirements for
          Latin Square solution (non-duplication across rows,
          columns, and boxes) */
        static bool solveLatinSquare(int[,] grid, int row,
                                int col)
        {

            /*if we have reached the 8th
                   row and 9th column (0
                   indexed matrix) ,
                   we are returning true to avoid further
                   backtracking       */
            if (row == N - 1 && col == N)
                return true;

            // Check if column value  becomes 9 ,
            // we move to next row
            // and column start from 0
            if (col == N)
            {
                row++;
                col = 0;
            }

            // Check if the current position
            // of the grid already
            // contains value >0, we iterate
            // for next column
            if (grid[row, col] != 0)
                return solveLatinSquare(grid, row, col + 1);

            for (int num = 1; num < N + 1; num++)
            {

                // Check if it is safe to place
                // the num (1-9)  in the
                // given row ,col ->we move to next column
                if (isSafe(grid, row, col, num))
                {

                    /*  assigning the num in the current
                            (row,col)  position of the grid and
                            assuming our assigned num in the position
                            is correct */
                    grid[row, col] = num;

                    // Checking for next
                    // possibility with next column
                    if (solveLatinSquare(grid, row, col + 1))
                        return true;
                }
                /* removing the assigned num , since our
                         assumption was wrong , and we go for next
                         assumption with diff num value   */
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

        // Check whether it will be legal
        // to assign num to the
        // given row, col
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
