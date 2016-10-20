using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{


    public class AdjacencyVertex<T> where T : IComparable
    {
        public T Key { get; set; }

        public AdjacencyEdge FirstEdge { get; set; }

        //某些算法用到,不属于自身性质
        public AdjacencyVertex<T> Parent { get; set; }

        public int Distance { get; set; }

        internal Color Color { get; set; }

        internal int FisrtVisitTime { get; set; }

        internal int FinalVisitTime { get; set; }


        public override string ToString()
        {
            return string.Format("Key: {0} Distance: {1} FisrtVisitTime:{2} FinalVisitTime:{3}"
            , Key
            , Distance
            , FisrtVisitTime
            , FinalVisitTime
            );
        }
    }

    internal enum Color
    {
        White,
        Gray,
        Black,

    }
    public class AdjacencyEdge
    {
        public int Start { get; set; }
        public int End { get; set; }

        public AdjacencyEdge Next { get; set; }

        public AdjacencyEdge()
        {

        }

        public AdjacencyEdge(int start, int end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return string.Format("Start: {0} End:{1}", Start, End);
        }
    }

    public class AdjacencyListGraph<T> : GraphBase<T> where T : IComparable
    {
        private IList<AdjacencyVertex<T>> _adjacencyList = new List<AdjacencyVertex<T>>();

        //public readonly bool HasDirection{get;set;}
        public bool HasDirection { get; set; }

        public AdjacencyListGraph()
        {

        }
        public AdjacencyListGraph(bool hasDirection)
        {
            HasDirection = hasDirection;
        }

        //BFS
        public void BreadthFirstSearch(AdjacencyVertex<T> source, Action<AdjacencyVertex<T>> action)
        {
            var vertexCount = _adjacencyList.Count;
            var sourceIndex = _adjacencyList.IndexOf(source);


            var grayQueue = new Queue<AdjacencyVertex<T>>();

            // 		Console.WriteLine("sourceIndex:{0} vertexCount:{1} colorsCount:{2}"
            // 			,sourceIndex
            // 			,vertexCount
            // 			,colors.Length
            // 		);

            for (int i = 0; i < vertexCount; i++)
            {
                var vertex = _adjacencyList[i];
                if (vertex == source)
                {
                    continue;
                }
                vertex.Distance = int.MaxValue;
                vertex.Parent = null;
                vertex.Color = Color.White;
            }

            source.Color = Color.Gray;
            source.Parent = null;
            source.Distance = 0;

            //Console.WriteLine("grayQueue start");

            grayQueue.Enqueue(source);
            while (!grayQueue.IsEmpty)
            {
                var startVertex = grayQueue.Dequeue();
                foreach (var edge in GetVertexEdge(startVertex))
                {
                    var endeEnd = edge.End;
                    var endVertex = _adjacencyList[endeEnd];
                    if (endVertex.Color == Color.White)
                    {
                        endVertex.Color = Color.Gray;
                        endVertex.Distance = startVertex.Distance + 1;
                        endVertex.Parent = startVertex;
                        grayQueue.Enqueue(endVertex);
                    }
                }
                startVertex.Color = Color.Black;
                action(startVertex);
            }
        }

        //DFS
        private int _time;
        public void DepthFirstSearch(Action<AdjacencyVertex<T>> action)
        {
            foreach (var item in _adjacencyList)
            {
                item.Color = Color.White;
                item.Parent = null;
            }
            _time = 0;

            foreach (var item in _adjacencyList)
            {
                if (item.Color == Color.White)
                {
                    DepthFirstSearchVisit(item, action);
                }
            }
        }

        private void DepthFirstSearchVisit(AdjacencyVertex<T> source, Action<AdjacencyVertex<T>> action)
        {
            source.Color = Color.Gray;
            _time = _time + 1;
            source.FisrtVisitTime = _time;

            // 		Console.WriteLine();
            // 		Console.Write("Visit  ");
            // 		
            // 		Console.Write("{0} ,",source);
            // 		Console.Write("time {0} ,",_time);

            var edges = GetVertexEdge(source);

            foreach (var item in edges)
            {
                // 			Console.WriteLine();
                // 			Console.Write("Edge   ");
                // 		
                // 			Console.Write("{0} ,",source);
                // 			Console.Write("{0} ,",item);
                // 			Console.Write("time {0} ,",_time);

                var vertex = _adjacencyList[item.End];
                if (vertex.Color == Color.White)
                {
                    vertex.Parent = source;
                    DepthFirstSearchVisit(vertex, action);
                }
            }

            source.Color = Color.Black;

            _time = _time + 1;
            source.FinalVisitTime = _time;

            action(source);
        }


        public IEnumerable<AdjacencyVertex<T>> TopologicalSort()
        {
            var result = new List<AdjacencyVertex<T>>();
            DepthFirstSearch((vertex) =>
            {
                result.Insert(0, vertex);
            });

            return result;
        }
        public void PrintPath(AdjacencyVertex<T> source, AdjacencyVertex<T> vertex)
        {
            if (source == vertex)
            {
                Console.WriteLine(vertex);
                return;
            }
            if (vertex.Parent == null)
            {
                Console.WriteLine("no path form source:{0} vertex:{1}", source, vertex);
            }
            else
            {
                PrintPath(source, vertex.Parent);
                Console.WriteLine(vertex);
            }
        }



        public IEnumerable<AdjacencyEdge> GetVertexEdge(AdjacencyVertex<T> vertex)
        {
            var edges = new List<AdjacencyEdge>();
            var edge = vertex.FirstEdge;
            while (edge != null)
            {
                edges.Add(edge);
                edge = edge.Next;
            }
            return edges;
        }

        public IEnumerable<AdjacencyVertex<T>> GetVertexs()
        {
            return _adjacencyList;
        }
        public AdjacencyVertex<T> GetVertexByKey(T key)
        {
            return _adjacencyList.Where(o => o.Key.CompareTo(key) == 0).FirstOrDefault();
        }

        public AdjacencyVertex<T> GetVertexByIndex(int index)
        {
            return _adjacencyList[index];
        }

        public int AddVertex(T key)
        {
            var vertex = new AdjacencyVertex<T>() { Key = key };
            _adjacencyList.Add(vertex);
            return _adjacencyList.Count;
        }

        public void AddEdge(int first, int second)
        {
            AddEdgeCore(first, second);
            if (!HasDirection)
            {
                AddEdgeCore(second, first);
            }
        }

        public void AddEdgeCore(int start, int end)
        {
            if (start > _adjacencyList.Count || end > _adjacencyList.Count)
            {
                throw new ArgumentOutOfRangeException("can't find vertex");
            }
            var startVertex = _adjacencyList[start];
            var edge = new AdjacencyEdge(start, end);

            if (startVertex.FirstEdge == null)
            {
                startVertex.FirstEdge = edge;
            }
            else
            {
                var pointerEdge = startVertex.FirstEdge;
                while (pointerEdge.Next != null)
                {
                    pointerEdge = pointerEdge.Next;
                }
                //竟然这里错了,写了这么多才发现，之前的测试不完备呢。 不是因为拓扑排序都不知道
                //pointerEdge=edge;
                pointerEdge.Next = edge;
            }
        }

    }
}