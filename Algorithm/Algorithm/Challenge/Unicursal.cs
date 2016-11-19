using Algorithm.Struct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Challenge
{
    public class Unicursal
    {

        private AdjacencyListGraph<int> _graph;

        public AdjacencyListGraph<int> Graph
        {
            get
            {
                return _graph;
            }
        }


        private int _xDimensionBound;

        public int XDimensionBound
        {
            get
            {
                return _xDimensionBound;
            }
        }



        private int _yDimensionBound;

        public int YDimensionBound
        {
            get
            {
                return _yDimensionBound;
            }
        }


        private Dictionary<int, List<List<int>>> _lengthPaths = new Dictionary<int, List<List<int>>>();

        public Dictionary<int, List<List<int>>> LengthPaths
        {
            get
            {
                return _lengthPaths;
            }
        }

        private int _length;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xDimensionBound"></param>
        /// <param name="yDimensionBound"></param>
        public void Create(int xDimensionBound, int yDimensionBound)
        {
            _xDimensionBound = xDimensionBound;
            _yDimensionBound = yDimensionBound;

            _length = xDimensionBound * yDimensionBound;

            _graph = new AdjacencyListGraph<int>();

            //这个创建都写了很久，靠调试。
            for (int i = 0; i < yDimensionBound; i++)
            {
                for (int j = 0; j < xDimensionBound; j++)
                {
                    AddVertex(j, i);
                }
            }

        }


        public void CalcPath()
        {
            for (int i = 2; i <= _length; i++)
            {
                if (i == 2)
                {
                    var paths = new List<List<int>>();
                    var edges = _graph.GetEdges();
                    foreach (var item in edges)
                    {
                        if (item.Start.Key < item.End.Key)
                        {
                            var path = new List<int>()
                            {
                                item.Start.Identifier,
                                item.End.Identifier
                            };
                            paths.Add(path);
                        }
                    }
                    _lengthPaths[i] = paths;
                }
                else
                {
                    var paths = new List<List<int>>();
                    var prePaths = _lengthPaths[i - 1];

                    foreach (var prePath in prePaths)
                    {
                        var startVertex = _graph.GetVertexByKey(prePath[0]);
                        var startEdges = _graph.GetVertexEdge(startVertex);
                        foreach (var startEdge in startEdges)
                        {
                            var endVertex = startEdge.End;
                            if (!prePath.Contains(endVertex.Identifier))
                            {
                                var path = new List<int>(prePath);
                                path.Insert(0, endVertex.Identifier);
                                paths.Add(path);
                            }
                        }
                    }


                    _lengthPaths[i] = paths;
                }

            }
        }


        /// <summary>
        /// 这样插入vertex,只有上面，左边，左上，右上4种vertex可能有边。
        /// </summary>
        /// <param name="vertexIndex"></param>
        private void AddVertex(int xDimension, int yDimension)
        {
            var key = GetVertexIndex(xDimension, yDimension) + 1;
            var vertex = _graph.CreateVertex(key);
            _graph.AddVertex(vertex);

            var count = 0;

            var upperLeft = GetVertex(xDimension - 1, yDimension - 1);
            if (upperLeft != null)
            {
                count++;
                _graph.AddEdge(vertex, upperLeft);
            }

            var upper = GetVertex(xDimension, yDimension - 1);
            if (upper != null)
            {
                count++;
                _graph.AddEdge(vertex, upper);
            }

            var upperRight = GetVertex(xDimension + 1, yDimension - 1);
            if (upperRight != null)
            {
                count++;
                _graph.AddEdge(vertex, upperRight);
            }

            var left = GetVertex(xDimension - 1, yDimension);
            if (left != null)
            {
                count++;
                _graph.AddEdge(vertex, left);
            }

            Console.WriteLine($"vertex:{key}  legeCount:{count}");
        }

        private int GetVertexIndex(int xDimension, int yDimension)
        {
            return xDimension + yDimension * _xDimensionBound;
        }

        private AdjacencyVertex<int> GetVertex(int xDimension, int yDimension)
        {
            if ((xDimension < 0 || xDimension >= _xDimensionBound) || (yDimension < 0 || yDimension >= _yDimensionBound))
            {
                return null;
            }

            var index = GetVertexIndex(xDimension, yDimension);


            return _graph.GetVertexByKey(index);
        }
    }
}
