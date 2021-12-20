using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_1._1_SocialGolfersProblem
{
    public class SGP_BFS
    {

        public SGP sgp = new SGP(new Representation
        {
            Layer1 = Enumerable.Range(1, 8).ToArray(),
            Layer2 = Enumerable.Range(9, 8).ToArray(),
            Layer3 = Enumerable.Range(17, 8).ToArray(),
            Layer4 = Enumerable.Range(25, 8).ToArray()
        });
        public bool Started { get; set; }
        public SGP_BFS()
        {

        }

        public bool CheckTarget(SGP tempSGP)
        {
            //{ Block code here}
            return true;
        }

        public List<SGP> GenerateChildren(SGP tempSGP)
        {
            SGP childSGP1 = new SGP(tempSGP.Representation);

            for (int i = 0; i < 4; i++)
            {
                //sjell majtas layer 1
                childSGP1.Level = tempSGP.Level + 1;
                childSGP1.Layer = 1;
                childSGP1.Parent = tempSGP;
                childSGP1.Representation = RotateLeft(childSGP1.Representation, 1);


                //sjell majtas layer 2
                childSGP1.Level = tempSGP.Level + 1;
                childSGP1.Layer = 2;
                childSGP1.Parent = tempSGP;
                childSGP1.Representation = RotateLeft(childSGP1.Representation, 2);
                childSGP1.Representation = RotateLeft(childSGP1.Representation, 2);

                //sjell majtas layer 3
                childSGP1.Level = tempSGP.Level + 1;
                childSGP1.Layer = 3;
                childSGP1.Parent = tempSGP;
                childSGP1.Representation = RotateLeft(childSGP1.Representation, 3);
                childSGP1.Representation = RotateLeft(childSGP1.Representation, 3);
                childSGP1.Representation = RotateLeft(childSGP1.Representation, 3);

                childSGP1.Level = tempSGP.Level + i + 1;
                tempSGP.Children.Add(childSGP1);
                tempSGP.Children.Add(ShiftColumn(childSGP1));

                childSGP1 = new SGP(childSGP1.Representation);
            }
            return tempSGP.Children;
        }


        public SGP ShiftColumn(SGP sgp)
        {
            var projection = ProjectRepresentation(sgp);
            var projectTemp = new int[4];
            for (int i = 1; i < projection.GetLength(0); i++)
            {
                for (int j = 0; j < projection.GetLength(1); j++)
                    projectTemp[j] = projection[i, (j + i) % projection.GetLength(1)];
                for (int j = 0; j < projection.GetLength(1); j++)
                    projection[i, j] = projectTemp[j];
            }
            return new SGP(new Representation(projection), 0);
        }

        public bool Compare(SGP candidateSGP, List<SGP> currentList, int layer = 0) => CheckValidationInsertion(currentList, candidateSGP, layer);

        public bool CompareGenerated(SGP candidateSGP, List<SGP> currentList, int layer = 0) => CheckValidationGenerationForInsertion(currentList, candidateSGP, layer);

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

        public void Print(List<SGP> tempSGP)
        {
            int week = 1;
            foreach (var item in tempSGP)
            {
                Console.WriteLine($"Week {week}:");
                for (int i = 0; i < item.Representation.Layer1.Length; i++)
                {
                    Console.Write($"Group {i + 1}:\t");
                    for (int j = 0; j < 4; j++)
                        switch (j)
                        {
                            case 0:
                                Console.Write(item.Representation.Layer1[i] + "  ");
                                break;
                            case 1:
                                Console.Write(item.Representation.Layer2[i] + "  ");
                                break;
                            case 2:
                                Console.Write(item.Representation.Layer3[i] + "  ");
                                break;
                            case 3:
                                Console.Write(item.Representation.Layer4[i] + "  ");
                                break;
                        }
                    Console.WriteLine("");
                }
                week++;
                Console.WriteLine("\n\n");
            }
            Console.WriteLine();
        }


        public Representation RotateLeft(Representation _representation, int layer)
        {
            var tempArr = new int[8];
            var representation = new Representation(_representation.Layer1, _representation.Layer2, _representation.Layer3, _representation.Layer4);
            switch (layer)
            {
                case 1:
                    Array.Copy(representation.Layer2, tempArr, tempArr.Length);
                    break;
                case 2:
                    Array.Copy(representation.Layer3, tempArr, tempArr.Length);
                    break;
                case 3:
                    Array.Copy(representation.Layer4, tempArr, tempArr.Length);
                    break;
            }

            var temp = tempArr[tempArr.Length - 1];
            for (int i = tempArr.Length - 1; i >= 1; i--)
                tempArr[i] = tempArr[i - 1];
            tempArr[0] = temp;

            switch (layer)
            {
                case 1:
                    representation.Layer2 = tempArr;
                    break;
                case 2:
                    representation.Layer3 = tempArr;
                    break;
                case 3:
                    representation.Layer4 = tempArr;
                    break;
            }

            return representation;
        }



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

        public bool CheckValidationGenerationForInsertion(List<SGP> nodeRounds, SGP nextNodeRound, int layer = 0)
        {
            var next = ProjectRepresentation(nextNodeRound);
            if (nodeRounds.Count == 0)
                layer = -1;
            //else layer = nodeRounds.Count - 1;// (layer > 0 ? layer : (nodeRounds.Count - 1));
            for (int i = 0; i < nodeRounds.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var previous = GetRow(j, ProjectRepresentation(nodeRounds[i]), layer);
                    for (int u = 0; u < 8; u++)
                    {
                        var result = CheckNodeGenerated(previous, GetRow(u, next, layer), layer);
                        if (result)
                            return true;
                    }
                }
            }
            return false;
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
                    if (!rountItem.Except(item).Any())
                        return true;
            return false;
        }

        public bool CheckNodeGenerated(int[] previous, int[] next, int layer = 0)
        {
            var previosVariationsList = GetVariations<int>(layer + 1, previous.Take(layer + 1).ToList());
            var nextList = GetVariations<int>(2, next.ToList());
            foreach (var rountItem in nextList)
                foreach (var item in previosVariationsList.ToList())
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
}
