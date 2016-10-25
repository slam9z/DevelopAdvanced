using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    public class AdjacencyMatrixNode<T> where T : IEquatable<T>
    {
        public int XCoordinate { get; set; }

        public int YCoordinate { get; set; }

        public int Weight { get; set; }

        public int PathWeight { get; set; }

        public  T Key { get; set; }

        public AdjacencyMatrixNode<T> Predecessor { get; set; }

        public AdjacencyMatrixNode()
        {

        }

        public AdjacencyMatrixNode(int weight)
        {
            Weight = weight;    
        }


        public override string ToString()
        {
            return $"Weight: {Weight} PathWeight:{PathWeight}";
        }
    }
}
