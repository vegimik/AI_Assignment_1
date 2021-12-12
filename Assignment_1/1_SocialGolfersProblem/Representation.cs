namespace Assignment_1._1_SocialGolfersProblem
{
    public class Representation
    {
        public int[] Layer1 { get; set; } = new int[8];
        public int[] Layer2 { get; set; } = new int[8];
        public int[] Layer3 { get; set; } = new int[8];
        public int[] Layer4 { get; set; } = new int[8];

        public Representation()
        {

        }

        public Representation(int[,] _representation)
        {
            for (int j = 0; j < _representation.GetLength(1); j++)
            {
                for (int i = 0; i < _representation.GetLength(0); i++)
                {
                    switch (j)
                    {
                        case 0:
                            Layer1[i] = _representation[i, j];
                            break;
                        case 1:
                            Layer2[i] = _representation[i, j];
                            break;
                        case 2:
                            Layer3[i] = _representation[i, j];
                            break;
                        case 3:
                            Layer4[i] = _representation[i, j];
                            break;
                    }
                }
            }
        }

        public Representation(
            int[] _Layer1,
            int[] _Layer2,
            int[] _Layer3,
            int[] _Layer4
            )
        {
            Layer1 = _Layer1;
            Layer2 = _Layer2;
            Layer3 = _Layer3;
            Layer4 = _Layer4;
        }
    }
}
