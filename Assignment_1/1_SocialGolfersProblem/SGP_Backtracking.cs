using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_1._1_SocialGolfersProblem
{
    public static class SGP_Backtracking
    {

        // Driver Code
        public static void Main_SGP_Backtracking()
        {

            int[,] board = new int[16, 4];
            //board[0, 0] = 1;
            //board[0, 1] = 9;
            //board[0, 2] = 17;
            //board[0, 3] = 25;
            //for (int i = 0; i < 8; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        board[i, j] = j*8+i+1;
            //    }

            //}
            //    int[,] board = new int[,] {
            //    { 1, 9,17,25},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0},
            //    { 0, 0, 0, 0}
            //};
            int N = board.GetLength(0);

            if (solveSudoku(board, N, 0))
            {

                // print solution
                var result = GetCurrentSolutions(board, 2);
                Print(result.Item1);
            }
            else
            {
                Console.Write("No solution");
            }
        }


        public static bool isSafe(int[,] board, int row, int col, int num)
        {
            var kRow = row / 8;


            if (row / 8 == 2)
            {

            }

            var res = GetCurrentSolutions(board, kRow);
            for (int j = 0; j < res.Item2.GetLength(1); j++)
            {
                for (int i = 0; i < res.Item2.GetLength(0); i++)
                {

                    if (res.Item2[i, j] == num)
                    {
                        if (i == row && j == col)
                            continue;
                        else
                            return false;
                    }
                }
            }

            return CheckValidationInsertion(res.Item1, res.Item2);
        }



        public static bool CheckValidationInsertion(List<int[,]> nodeRounds, int[,] nextNodeRound, int layer = 0)
        {
            var next = nextNodeRound;
            if (nodeRounds.Count == 0)
                layer = -1;
            else layer = nodeRounds.Count - 1;
            for (int i = 0; i < nodeRounds.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var previous = GetRow(j, nodeRounds[i], layer);
                    for (int u = 0; u < 8; u++)
                    {
                        var result = CheckNode(previous, GetRow(u, next, layer), layer);
                        if (result)
                            return false;
                    }
                }
            }
            return true;
        }

        public static int[] GetRow(int row, int[,] representation, int layer = 0)
        {
            var rowData = new int[4];
            for (int i = 0; i <= 3; i++)
                rowData[i] = representation[row, i];
            return rowData;
        }


        public static (List<int[,]>, int[,]) GetCurrentSolutions(int[,] listBoard, int kRow)
        {
            bool validBoard = true;
            List<int[,]> boardListStatus = new List<int[,]>();

            for (int i = 0; i < kRow; i++)
            {
                var modelBoard = new int[8, 4];
                for (int j = 0; j < 8; j++)
                {
                    for (int u = 0; u < 4; u++)
                    {
                        if (listBoard[i * 8 + j, u] == 0)
                            validBoard = false;
                        modelBoard[j, u] = listBoard[i * 8 + j, u];
                    }
                }
                if (!validBoard)
                    break;
                boardListStatus.Add(modelBoard);
            }

            var currentBoard = new int[8, 4];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i + (kRow) * 8 == 16)
                    {
                        kRow = kRow - 1;
                    }
                    currentBoard[i, j] = listBoard[i + (kRow) * 8, j];
                }
            }

            return (boardListStatus, currentBoard);
        }




        public static bool CheckNode(int[] previous, int[] next, int layer = 0)
        {
            var previosVariationsList = GetVariations<int>(4, previous.ToList());
            var nextList = GetVariations<int>(2, next.ToList());
            foreach (var rountItem in nextList)
                foreach (var item in previosVariationsList.ToList())
                    if (!rountItem.Except(item).Any())
                        return true;
            return false;
        }


        public static List<List<T>> GetVariations<T>(int k, List<T> elements)
        {
            List<List<T>> result = new List<List<T>>();
            if (k == 1)
                result.AddRange(elements.Select(element => new List<T>() { element }));
            else
            {
                foreach (T element in elements)
                {
                    List<T> subelements = elements.Where(e => !e.Equals(element)).ToList();
                    List<List<T>> subvariations = GetVariations(k - 1, subelements);
                    foreach (List<T> subvariation in subvariations)
                    {
                        subvariation.Add(element);
                        result.Add(subvariation);
                    }
                }
            }
            return result;
        }


        public static bool solveSudoku(int[,] board, int n, int startRow = 0)
        {
            int row = -1;
            int col = -1;
            bool isEmpty = true;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    break;
                }
            }

            // no empty space left
            if (isEmpty)
            {
                return true;
            }

            // else for each-row backtrack
            for (int num = 1; num <= 32; num++)
            {
                if (isSafe(board, row, col, num))
                {
                    //if (row == 15)
                    //{

                    //}
                    board[row, col] = num;
                    if (solveSudoku(board, n, (row / 8) * 8))
                    {
                        // Print(board, n);
                        return true;
                    }
                    else
                    {
                        // Replace it
                        board[row, col] = 0;
                    }
                }
            }
            return false;
        }


        public static void Print(List<int[,]> listBoards)
        {
            int week = 1;
            foreach (var item in listBoards)
            {
                Console.WriteLine($"Week {week}:");
                for (int i = 0; i < item.GetLength(0); i++)
                {
                    Console.Write($"Group {i + 1}:\t");
                    for (int j = 0; j < item.GetLength(1); j++)
                        Console.Write(item[i, j] + "  ");
                    Console.WriteLine("");
                }
                week++;
                Console.WriteLine("\n\n");
            }
            Console.WriteLine();
        }

        public static void print(int[,] board, int N)
        {

            // We got the answer, just print it
            for (int r = 0; r < N; r++)
            {
                for (int d = 0; d < N; d++)
                {
                    Console.Write(board[r, d]);
                    Console.Write(" ");
                }
                Console.Write("\n");

                if ((r + 1) % (int)Math.Sqrt(N) == 0)
                {
                    Console.Write("");
                }
            }
        }

    }
}
