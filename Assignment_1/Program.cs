using Assignment_1._2_LatinSquare;
using System;
using System.Linq;

namespace Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        {
            LatinSquare_BackTracking.SetLatinSquareGrid(4);
            LatinSquare_BackTracking.Main_LatinSquare_BackTracking();

            Console.WriteLine("My World!");
            Console.ReadLine();
        }
    }
}
