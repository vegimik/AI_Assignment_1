using System.Collections.Generic;

namespace Assignment_1._2_LatinSquare
{
    public class LatinSquare
    {
        public int Level;
        public int[] Representation { get; set; }
        public LatinSquare Parent;
        public List<LatinSquare> Children;

        public LatinSquare(int[] _representation)
        {
            Representation = new int[_representation.Length];
            for (int i = 0; i < _representation.GetLength(0); i++)
                Representation[i] = _representation[i];

            Children = new List<LatinSquare>();
            Level = 0;
        }
    }
}
