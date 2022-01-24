using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SGP = Assignment_1._1_SocialGolfersProblem.SGP;
using Representation = Assignment_1._1_SocialGolfersProblem.Representation;

namespace Assignment_1
{
    public class SGP_Validator
    {
        int n_groups = 8;
        int n_per_group = 4;

        public void CheckSolution()
        {
            var isValidSolution = Validator(FetchData(ReadAllFile()));
            Console.WriteLine(isValidSolution);
        }

        public string ReadAllFile()
        {
            string contents = "";
            contents = File.ReadAllText(@"C:\Users\vegim\Desktop\validators.txt");
            return contents;
        }


        //Queue<SGP> OpenList = new Queue<SGP>();

        public List<SGP> FetchData(string content)
        {
            var list = new List<SGP>();
            var lines = content.Split("\n");
            for (int i = 0; i < lines.Length; i++)
            {
                var groups = lines[i].Split("   ");
                var modelRepresentation = new int[n_groups, n_per_group];
                for (int u = 0; u < groups.Length; u++)
                {
                    var players = groups[u].Replace("\r", "").Split(" ");
                    int counter = 0, row = 0, col = 0;
                    while (counter < n_groups * n_per_group + n_groups - 1)
                    {
                        if (players[counter] != "")
                        {
                            modelRepresentation[row, col] = Int32.Parse(players[counter]);
                            col++;
                        }
                        if (counter % (n_per_group + 1) == n_per_group)
                        {
                            row += 1;
                            col = 0;
                        }
                        counter++;
                    }
                }
                list.Add(new SGP(new Representation(modelRepresentation), 0));
            }
            return list;
        }


        public bool Validator(List<SGP> list)
        {
            Queue<SGP> OpenList = new Queue<SGP>();
            List<SGP> ClosedList = new List<SGP>();

            OpenList.AddRange(list);

            SGP tempSGP = null;
            while (OpenList.Count > 0)
            {
                tempSGP = OpenList.Dequeue();
                if (Compare(tempSGP, ClosedList) == false)
                    ClosedList.Add(tempSGP);
                else
                    return false;
            }

            return true;
        }

        public bool Compare(SGP candidateSGP, List<SGP> currentList, int layer = 0) => CheckValidationInsertion(currentList, candidateSGP, layer);

        public bool CheckValidationInsertion(List<SGP> nodeRounds, SGP nextNodeRound, int layer = 0)
        {
            var next = ProjectRepresentation(nextNodeRound);
            if (nodeRounds.Count == 0)
                layer = -1;
            else layer = nodeRounds.Count - 1;
            for (int i = 0; i < nodeRounds.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var previous = GetRow(j, ProjectRepresentation(nodeRounds[i]), layer);
                    for (int u = 0; u < 8; u++)
                    {
                        var result = CheckNode(previous, GetRow(u, next, layer), layer);
                        if (result)
                            return true;
                    }
                }
            }
            return false;
        }

        public int[,] ProjectRepresentation(SGP sgp)
        {
            int proj = 0;
            int[,] projection = new int[8, 4];
            for (int j = 0; j < projection.GetLength(1); j++)
            {
                for (int i = 0; i < projection.GetLength(0); i++)
                {
                    switch (j)
                    {
                        case 0:
                            proj = sgp.Representation.Layer1[i];
                            break;
                        case 1:
                            proj = sgp.Representation.Layer2[i];
                            break;
                        case 2:
                            proj = sgp.Representation.Layer3[i];
                            break;
                        case 3:
                            proj = sgp.Representation.Layer4[i];
                            break;
                    }
                    projection[i, j] = proj;
                }
            }
            return projection;
        }

        public int[] GetRow(int row, int[,] representation, int layer = 0)
        {
            var rowData = new int[4];
            for (int i = 0; i <= 3 /*(layer > 0 ? layer : 3)*/; i++)
                rowData[i] = representation[row, i];
            return rowData;
        }


        public bool CheckNode(int[] previous, int[] next, int layer = 0)
        {
            var previosVariationsList = GetVariations<int>(4, previous.ToList());
            var nextList = GetVariations<int>(2, next.ToList());
            foreach (var rountItem in nextList)
                foreach (var item in previosVariationsList.ToList())
                    if (rountItem.Where(x => x == 0).Count() == 0)
                        if (!rountItem.Except(item).Any())
                            return true;
            return false;
        }

        public List<List<T>> GetVariations<T>(int k, List<T> elements)
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

    }


    public static class HelperFunctions
    {
        public static void AddRange<T>(this Queue<T> queue, IEnumerable<T> enu)
        {
            foreach (T obj in enu)
                queue.Enqueue(obj);
        }

    }
}
