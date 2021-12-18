using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_1._2_LatinSquare
{
    public static class HelperFunctions
    {
        public static void AddRange<T>(this Queue<T> queue, IEnumerable<T> enu)
        {
            foreach (T obj in enu)
                queue.Enqueue(obj);
        }

    }
}
