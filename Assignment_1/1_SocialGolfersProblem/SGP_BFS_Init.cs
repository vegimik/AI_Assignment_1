using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_1._1_SocialGolfersProblem
{
    public static class SGP_BFS_Init
    {
        public static void Init_Main()
        {
            var sgp_BFS_obj = new SGP_BFS();
            var timeConsumed = false;

            Queue<SGP> OpenList = new Queue<SGP>();
            List<SGP> ClosedList = new List<SGP>();

            OpenList.Enqueue(sgp_BFS_obj.sgp);

            SGP tempSGP = null;
            while (OpenList.Count > 0)
            {
                tempSGP = OpenList.Dequeue();
                if (/*sgp_BFS_obj.Compare(tempSGP, OpenList.ToList(), tempSGP.Layer) == false && */sgp_BFS_obj.Compare(tempSGP, ClosedList) == false)
                    ClosedList.Add(tempSGP);

                //Let's do a barrier
                if (OpenList.Count > 200)
                    timeConsumed = true;

                //Generate children
                if ((!(tempSGP.Representation.Layer2 == tempSGP.PresentationLayer) || !(tempSGP.Representation.Layer3 == tempSGP.PresentationLayer) || !(tempSGP.Representation.Layer4 == tempSGP.PresentationLayer)) && !timeConsumed && tempSGP.Level <= 8)
                    tempSGP.Children = sgp_BFS_obj.GenerateChildren(tempSGP);

                for (int i = 0; i < tempSGP.Children.Count; i++)
                {
                    SGP candidateSGP = tempSGP.Children[i];
                    candidateSGP.Level = tempSGP.Children[i].Level;
                    if (sgp_BFS_obj.CompareGenerated(candidateSGP, OpenList.ToList(), candidateSGP.Layer) == false && sgp_BFS_obj.CompareGenerated(candidateSGP, ClosedList, candidateSGP.Layer) == false)
                        OpenList.Enqueue(candidateSGP);
                }
            }

            //print solution
            sgp_BFS_obj.Print(ClosedList);

            Console.ReadKey();
        }

    }
}
