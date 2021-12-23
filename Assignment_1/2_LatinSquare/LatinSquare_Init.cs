using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_1._2_LatinSquare
{
    public class LatinSquare_Init
    {
        public static void Drive()
        {
            Console.WriteLine("Welcome to the LatinSquare:");
            Console.WriteLine("Choose any method for trying to solve this game\n[1-BFS, 2-DFS, 3-BackTracking, 4-ForwardChecking, 11-BFS(version2) and 22-DFS(version2)]: ");
            var input = Console.ReadLine();
            Console.WriteLine("\n");
            var number = 0;
            if (Int32.TryParse(input, out number))
            {
                switch (number)
                {
                    case 1://BFS
                        LatinSquare_BFS.SetLatinSquares(14);
                        LatinSquare_BFS.Main_LatinSquare();
                        break;
                    case 2://DFS
                        LatinSquare_DFS.SetLatinSquares(14);
                        LatinSquare_DFS.Main_LatinSquare();
                        break;
                    case 3://Backtracking
                        LatinSquare_BackTracking.SetLatinSquareGrid(14);
                        LatinSquare_BackTracking.Main_LatinSquare_BackTracking();
                        break;
                    case 4://Forwardchecking
                        LatinSquare_ForwardChecking.SetLatinSquareGrid(14);
                        LatinSquare_ForwardChecking.Main_LatinSquare_ForwardChecking();
                        break;
                    case 11://BFS version 2
                        LatinSquare_BFS_v2.SetLatinSquares(4);
                        LatinSquare_BFS_v2.Main_LatinSquare();
                        break;
                    case 22://DFS version 2
                        LatinSquare_DFS_v2.SetLatinSquares(4);
                        LatinSquare_DFS_v2.Main_LatinSquare();
                        break;
                }
            }
            else
                Console.WriteLine("Please, choose any method!");
        }
    }
}
