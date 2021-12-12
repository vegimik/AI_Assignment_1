//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Assignment_1._1_SocialGolfersProblem
//{
//    public static class SGP_Helper
//    {
//        public static bool CheckValidationInsertionSuccess(List<SGP> nodeRounds, SGP nextSGP)
//        {
//            var next = nextSGP.Representation;
//            for (int i = 0; i < nodeRounds.Count; i++)
//            {
//                for (int j = 0; j < 8; j++)
//                {
//                    var previous = GetRow(j, nodeRounds[i].Representation);
//                    for (int u = 0; u < 8; u++)
//                    {
//                        var result = CheckNode(previous, GetRow(u, next));
//                        if (result)
//                            return false;
//                    }
//                }
//            }
//            return true;
//        }

//        public static (List<SGP> ListValid, List<SGP> ListInValid) CheckValidationInsertion(List<SGP> nodeRounds, SGP nextSGP)
//        {
//            var ValidList = new List<SGP>();
//            var InValidList = new List<SGP>();
//            var next = nextSGP.Representation;
//            var valid = true;
//            for (int i = 0; i < nodeRounds.Count; i++)
//            {
//                for (int j = 0; j < 8; j++)
//                {
//                    var previous = GetRow(j, nodeRounds[i].Representation);
//                    for (int u = 0; u < 8; u++)
//                    {
//                        var result = CheckNode(previous, GetRow(u, next));
//                        if (result)
//                        {
//                            InValidList.Add(nodeRounds[i]);
//                            valid = false;
//                            break;
//                        }
//                    }
//                    if (!valid)
//                        break;
//                }
//                if (valid)
//                    ValidList.Add(nodeRounds[i]);
//                else
//                    break;
//            }
//            return (ValidList, InValidList);
//        }

//        public static bool CheckValidationInsertionProceed(List<int[,]> nodeRounds, int[,] nextSGP)
//        {
//            for (int i = 0; i < nodeRounds.Count; i++)
//            {
//                for (int j = 0; j < 8; j++)
//                {
//                    var previous = GetRow(j, nodeRounds[i]);
//                    for (int u = 0; u < 8; u++)
//                    {
//                        var result = CheckNode(previous, GetRow(u, nextSGP));
//                        if (result) return false;
//                    }
//                }
//            }
//            return true;
//        }

//        public static (List<SGP> ListValid, List<SGP> ListInValid) CheckValidation(List<SGP> nodeRounds)
//        {
//            var ValidList = new List<SGP>();
//            var InValidList = new List<SGP>();
//            for (int i = 0; i < nodeRounds.Count; i++)
//            {
//                for (int j = 0; j < i + 1; j++)
//                {
//                    var next = GetRow(j, nodeRounds[i].Representation);
//                    var previous = GetRow(j, nodeRounds[j].Representation);
//                    var result = CheckNode(previous, next);
//                    if (!result)
//                        ValidList.Add(nodeRounds[i]);
//                    else
//                        InValidList.Add(nodeRounds[i]);
//                }
//            }
//            return (ValidList, InValidList);
//        }

//        public static int[] GetRow(int row, int[,] representation)
//        {
//            var rowData = new int[4];
//            for (int i = 0; i < 4; i++)
//                rowData[i] = representation[row, i];
//            return rowData;
//        }

//        public static bool CheckNode(int[] previous, int[] next)//true kur e permban ate row,
//        {
//            var previosVariationsList = GetVariations(3, previous.ToList());
//            var nextList = GetVariations(2, next.ToList());
//            foreach (var rountItem in nextList)
//            {
//                foreach (var item in previosVariationsList.Select(x => string.Join("", x.ToList())))
//                {
//                    if (item.Contains(string.Join("", rountItem)))
//                    { return true; }

//                }
//            }
//            return false;
//        }

//        public static List<List<int>> CleanUpVariations(List<List<int>> list)
//        {
//            var result = false;
//            var ValidList = new List<List<int>>();
//            var InValidList = new List<List<int>>();
//            for (int i = 0; i < list.Count; i++)
//            {
//                for (int j = 0; j < ValidList.Count; j++)
//                {
//                    result = CheckNode(ValidList[j].ToArray(), list[i].ToArray());

//                }

//                if (!result)
//                    ValidList.Add(list[i]);
//                //else
//                //    InValidList.Add(nodeRounds[i]);
//            }
//            return ValidList;
//        }


//        public static List<List<int>> GetVariations(int k, List<int> elements)
//        {
//            List<List<int>> result = new List<List<int>>();
//            if (k == 1)
//            {
//                result.AddRange(elements.Select(element => new List<int> { element }));
//            }
//            else
//            {
//                foreach (var element in elements)
//                {
//                    var subelements = elements.Where(e => !e.Equals(element)).ToList();
//                    List<List<int>> subvariations = GetVariations(k - 1, subelements);
//                    foreach (var subvariation in subvariations)
//                    {
//                        subvariation.Add(element);
//                        result.Add(subvariation);
//                        //if (subvariation.Count == 1)
//                        //    break;
//                    }
//                }
//            }
//            return result;
//        }

//        public static List<List<int>> getvarioations_v2(List<int> elements)
//        {
//            var res = new List<List<int>>();
//            for (int i = elements.First(); i < elements.Count; i++)
//            {
//                for (int j = elements.First(); j < elements.Count; j++)
//                {
//                    if (i == j)
//                        continue;
//                    else
//                        for (int z = elements.First(); z < elements.Count; z++)
//                        {
//                            if (j == z)
//                                continue;
//                            else
//                                res.Add(new List<int> { i, j, z });
//                        }

//                }

//            }
//            return res;
//        }
//    }
//}
