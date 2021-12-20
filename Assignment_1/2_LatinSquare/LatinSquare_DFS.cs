using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_1._2_LatinSquare
{
    public static class LatinSquare_DFS
    {
        public static LatinSquare latinSquare { get; set; }

        public static void SetLatinSquares(int n)
        {
            var list = new int[n];
            if (n > 1)
                list = Enumerable.Range(1, n).ToArray();
            else
                list = Enumerable.Range(1, 5).ToArray();
            latinSquare = new LatinSquare(list);
        }

        //Drive Method
        public static void Main_LatinSquare()
        {
            Stack<LatinSquare> OpenList = new Stack<LatinSquare>();
            List<LatinSquare> ClosedList = new List<LatinSquare>();

            OpenList.Push(latinSquare);

            LatinSquare tempNode = null;
            while (OpenList.Count > 0)//perderisa ka nyje qe nuk jane vizitu
            {
                tempNode = OpenList.Pop();
                ClosedList.Add(tempNode);

                //kontrollo a kemi arrit tek target state
                if (CheckTarget_LatinSquare(tempNode, latinSquare) && ClosedList.Count > 1)
                {
                    Console.WriteLine("Zgjidhja u gjet ne nivelin " + tempNode.Level);
                    break;
                }

                //gjenerimi i children
                tempNode.Children = GenerateChildren_LatinSquare(tempNode);

                for (int i = 0; i < tempNode.Children.Count; i++)
                {
                    LatinSquare candidateNode = tempNode.Children[i];
                    if (Compare_LatinSqaure(candidateNode, OpenList.ToList()) == false && Compare_LatinSqaure(candidateNode, ClosedList) == false)
                        OpenList.Push(candidateNode);
                }
            }

            //printojme zgjidhjen
            Print_LatinSquare(ClosedList);
        }

        static bool CheckTarget_LatinSquare(LatinSquare baseCase, LatinSquare nextRound)
        {
            for (int i = 0; i < nextRound.Representation.GetLength(0); i++)
                if (baseCase.Representation[i] == nextRound.Representation[i])
                    return true;
            return false;
        }


        static List<LatinSquare> GenerateChildren_LatinSquare(LatinSquare tempNode)
        {
            List<LatinSquare> Children = new List<LatinSquare>();

            //levizja majtas
            {
                LatinSquare childNode = new LatinSquare(tempNode.Representation);
                childNode.Level = tempNode.Level + 1;
                childNode.Parent = tempNode;
                var temp = 0;
                for (int i = 0; i < childNode.Representation.GetLength(0); i++)
                {
                    if (i == 0)
                        temp = childNode.Representation[i];
                    var indx = (i + 1) % childNode.Representation.GetLength(0);
                    childNode.Representation[i] = childNode.Representation[indx];
                }
                childNode.Representation[childNode.Representation.GetLength(0) - 1] = temp;
                Children.Add(childNode);
            }

            //levizja djathtas
            {
                LatinSquare childNode = new LatinSquare(tempNode.Representation);
                childNode.Level = tempNode.Level + 1;
                childNode.Parent = tempNode;
                var temp = 0;
                for (int i = childNode.Representation.GetLength(0) - 1; i >= 0; i--)
                {
                    if (i == childNode.Representation.GetLength(0) - 1)
                        temp = childNode.Representation[0];
                    var indx = (i + 1) % childNode.Representation.GetLength(0);
                    childNode.Representation[indx] = childNode.Representation[i];
                }
                childNode.Representation[1] = temp;
                Children.Add(childNode);
            }

            return Children;
        }

        static bool Compare_LatinSqaure(LatinSquare candidateNode, List<LatinSquare> currentList)
        {
            bool NodeExist = false;
            for (int k = 0; k < currentList.Count; k++)
            {
                NodeExist = true;
                for (int i = 0; i < candidateNode.Representation.GetLength(0); i++)
                    if (candidateNode.Representation[i] != currentList[k].Representation[i])
                        NodeExist = false;

                if (NodeExist)
                    return true;
            }
            return false;
        }

        static void Print_LatinSquare(List<LatinSquare> closedList)
        {
            foreach (var item in closedList)
            {
                for (int i = 0; i < item.Representation.GetLength(0); i++)
                {
                    Console.Write(item.Representation[i] + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}
