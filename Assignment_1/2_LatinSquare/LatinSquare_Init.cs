﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_1._2_LatinSquare
{
    public class LatinSquare_Init
    {
        public static void Drive()
        {
            Console.WriteLine("Welcome to the LatinSquare:");
            Console.WriteLine("Choose any method for trying to solve this game\n[1-BFS, 2-DFS, 3-BackTracking and 4-ForwardChecking]: ");
            var input = Console.ReadLine();
            var number = 0;
            if (Int32.TryParse(input, out number))
            {
                switch (number)
                {
                    case 1:
                        LatinSquare_BFS.SetLatinSquares(4);
                        LatinSquare_BFS.Main_LatinSquare();
                        break;
                    case 2:
                        LatinSquare_DFS.SetLatinSquares(4);
                        LatinSquare_DFS.Main_LatinSquare();
                        break;
                    case 3:
                        LatinSquare_BackTracking.SetLatinSquareGrid(4);
                        LatinSquare_BackTracking.Main_LatinSquare_BackTracking();
                        break;
                    case 4:
                        //BFS
                        break;
                }
            }
            else
                Console.WriteLine("Please, choose any method!");
        }
    }
}