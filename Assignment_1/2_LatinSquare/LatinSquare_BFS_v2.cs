using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_1._2_LatinSquare
{
    public static class LatinSquare_BFS_v2
    {
        public static LatinSquare_v2 latinSquare { get; set; }
        public static int N;
        public static void SetLatinSquares(int n)
        {
            var list = new int[n];
            N = n;
            if (n > 1)
                list = Enumerable.Range(0, 1).ToArray();
            else
                list = Enumerable.Range(0, 1).ToArray();
            var formattedList = new int[N + 1];
            Array.Copy(list, formattedList, list.Length);
            latinSquare = new LatinSquare_v2(formattedList);
        }

        public static void Main_LatinSquare()
        {
            Queue<LatinSquare_v2> OpenList = new Queue<LatinSquare_v2>();
            List<LatinSquare_v2> ClosedList = new List<LatinSquare_v2>();

            OpenList.Enqueue(latinSquare);

            LatinSquare_v2 tempNode = null;
            while (OpenList.Count > 0)//perderisa ka nyje qe nuk jane vizitu
            {
                tempNode = OpenList.Dequeue();
                if (tempNode.Representation[1]!=0&&tempNode.Representation[4]!=0)
                {

                }
                if (Compare_LatinSqaure_v2(tempNode, ClosedList.ToList()))
                    ClosedList.Add(tempNode);

                //gjenerimi i children
                tempNode.Children = GenerateChildren_LatinSquare(tempNode);

                for (int i = 0; i < tempNode.Children.Count; i++)
                {
                    LatinSquare_v2 candidateNode = tempNode.Children[i];
                    //if (Compare_LatinSqaure(candidateNode, OpenList.ToList()) == false && Compare_LatinSqaure(candidateNode, ClosedList) == false)
                        OpenList.Enqueue(candidateNode);
                }
            }

            //printojme zgjidhjen
            Print_LatinSquare(ClosedList);
        }

        static bool CheckTarget_LatinSquare(LatinSquare_v2 baseCase, LatinSquare_v2 nextRound)
        {
            for (int i = 0; i < nextRound.Representation.GetLength(0); i++)
                if (baseCase.Representation[i] == nextRound.Representation[i])
                    return true;
            return false;
        }

        static List<LatinSquare_v2> GenerateChildren_LatinSquare(LatinSquare_v2 tempNode)
        {
            List<LatinSquare_v2> Children = new List<LatinSquare_v2>();
            var candidateNodes = new Queue<int>();
            candidateNodes.AddRange<int>(Enumerable.Range(1, N).Where(x => !tempNode.Representation.ToList().Contains(x)).ToList());
            var length = candidateNodes.Count;
            for (int i = 0; i < length; i++)
            {
                var node = candidateNodes.Dequeue();
                //generate next node
                var _representation = new int[N + 1];
                Array.Copy(tempNode.Representation, _representation, tempNode.Representation.Length);
                _representation[tempNode.Level + 1] = node;

                LatinSquare_v2 childNode = new LatinSquare_v2(_representation);
                childNode.Level = tempNode.Level + 1;
                childNode.Parent = tempNode;
                //childNode.Representation = childNode.Parent.Representation;
                //childNode.Representation[childNode.Level + 1] = node;
                Children.Add(childNode);

            }

            return Children;
        }

        static bool Compare_LatinSqaure(LatinSquare_v2 candidateNode, List<LatinSquare_v2> currentList)
        {
            bool NodeExist = false;
            for (int k = 0; k < currentList.Count; k++)
            {
                NodeExist = true;
                for (int i = 0; i < candidateNode.Representation.GetLength(0); i++)
                    if (candidateNode.Representation[i] != currentList[k].Representation[i]&& candidateNode.Representation[i]!=0)
                        NodeExist = false;

                if (NodeExist)
                    return true;
            }
            return false;
        }

        static bool Compare_LatinSqaure_v2(LatinSquare_v2 candidateNode, List<LatinSquare_v2> currentList)
        {
            if (candidateNode.Representation.Where(x => x==0).ToList().Count > 1)
                return false;

            for (int k = 0; k < currentList.Count; k++)
            {
                for (int i = 0; i < candidateNode.Representation.GetLength(0); i++)
                    if (candidateNode.Representation[i] == currentList[k].Representation[i] && candidateNode.Representation[i] != 0)
                        return false;
            }
            return true;
        }

        static void Print_LatinSquare(List<LatinSquare_v2> closedList)
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
