using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_1._1_SocialGolfersProblem
{
    public class SGP_Init
    {
        public static void Drive()
        {
            Console.WriteLine("Welcome to the Social Game Player:");
            Console.WriteLine("Choose any method for trying to solve this game\n[1-BFS, 2-DFS, 3-BackTracking and 4-ForwardChecking]: ");
            var input = Console.ReadLine();
            Console.WriteLine("\n");
            var number = 0;
            if (Int32.TryParse(input, out number))
            {
                switch (number)
                {
                    case 1://BFS
                        SGP_BFS_Init.Init_Main();
                        break;
                    case 2://DFS
                        SGP_DFS_Init.Init_Main();
                        break;
                    case 3://Backtracking
                        SGP_Backtracking.Main_SGP_Backtracking();
                        break;
                    case 4://Forwardchecking
                        SGP_ForwardChecking.Main_SGP_ForwardChecking();
                        break;
                }
            }
            else
                Console.WriteLine("Please, choose any method!");
        }
    }
}
