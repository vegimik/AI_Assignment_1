using System.Collections.Generic;

namespace Assignment_1._2_LatinSquare
{
    public class LatinSquare_v2
    {
        public int Level;
        public int[] Representation { get; set; }
        public LatinSquare_v2 Parent;
        public List<LatinSquare_v2> Children;

        public LatinSquare_v2(int[] _representation)
        {
            Representation = new int[_representation.Length];
            for (int i = 0; i < _representation.GetLength(0); i++)
                Representation[i] = _representation[i];

            Children = new List<LatinSquare_v2>();
            Level = 0;
        }
    }
}
