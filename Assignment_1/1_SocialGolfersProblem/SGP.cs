using System.Collections.Generic;

namespace Assignment_1._1_SocialGolfersProblem
{
    public class SGP
    {
        public Representation Representation = new Representation();
        public int[] PresentationLayer { get; set; } = new int[4];
        public SGP Parent;
        public int Layer;
        public int Level;
        public bool CanCombine;
        public List<SGP> Children;

        public SGP(Representation _representation)
        {
            Representation = new Representation(_representation.Layer1, _representation.Layer2, _representation.Layer3, _representation.Layer4);
            Children = new List<SGP>();
            Level = 0;
        }

        public SGP(Representation _representation, int _level) : this(_representation)
        {
            Level = _level;
        }

        public SGP(Representation _representation, int[] _presentationLayer, SGP _parent, int _layer) : this(_representation)
        {
            PresentationLayer = _presentationLayer;
            Parent = _parent;
            Layer = _layer;
        }
    }
}
