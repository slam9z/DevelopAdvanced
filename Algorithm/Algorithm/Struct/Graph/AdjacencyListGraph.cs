using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{

    public class AdjacencyListGraph<T> : GraphBase<T> where T : IComparable
    {
        private IDictionary<int, AdjacencyVertex<T>>
            _adjacencyDictionary = new Dictionary<int, AdjacencyVertex<T>>();

        public readonly bool HasDirection;

        public AdjacencyListGraph()
        {

        }

        public AdjacencyListGraph(bool hasDirection)
        {
            HasDirection = hasDirection;
        }



        #region   //BFS

        public void BreadthFirstSearch(AdjacencyVertex<T> source, Action<AdjacencyVertex<T>> finalVisitAction)
        {
            var grayQueue = new Queue<AdjacencyVertex<T>>();

            foreach (var vertex in _adjacencyDictionary.Values)
            {
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

            grayQueue.Enqueue(source);
            while (!grayQueue.IsEmpty)
            {
                var startVertex = grayQueue.Dequeue();
                foreach (var edge in GetVertexEdge(startVertex))
                {
                    var endeEnd = edge.End;
                    var endVertex = _adjacencyDictionary[endeEnd];
                    if (endVertex.Color == Color.White)
                    {
                        endVertex.Color = Color.Gray;
                        endVertex.Distance = startVertex.Distance + 1;
                        endVertex.Parent = startVertex;
                        grayQueue.Enqueue(endVertex);
                    }
                }
                startVertex.Color = Color.Black;
                finalVisitAction(startVertex);
            }
        }

        #endregion


        #region  //DFS

        private int _time;
        public void DepthFirstSearch(Action<AdjacencyVertex<T>> finalVisitAction = null)
        {
            foreach (var item in _adjacencyDictionary.Values)
            {
                item.Color = Color.White;
                item.Parent = null;
            }
            _time = 0;

            foreach (var item in _adjacencyDictionary.Values)
            {
                if (item.Color == Color.White)
                {
                    DepthFirstSearchVisit(item, finalVisitAction);
                }
            }
        }



        private void DepthFirstSearchVisit(AdjacencyVertex<T> source, Action<AdjacencyVertex<T>> finalVisitAction)
        {
            source.Color = Color.Gray;
            _time = _time + 1;
            source.FisrtVisitTime = _time;

            var edges = GetVertexEdge(source);

            foreach (var item in edges)
            {
                var vertex = _adjacencyDictionary[item.End];
                if (vertex.Color == Color.White)
                {
                    vertex.Parent = source;
                    DepthFirstSearchVisit(vertex, finalVisitAction);
                }
            }

            source.Color = Color.Black;

            _time = _time + 1;
            source.FinalVisitTime = _time;

            finalVisitAction?.Invoke(source);
        }

        #endregion

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

        /// <summary>
        /// 输出有点不明啊
        /// </summary>
        public IEnumerable<AdjacencyListGraph<T>> GetStronglyConnectedComponenets()
        {
            var stronglyConnectedGraphs = new List<AdjacencyListGraph<T>>();

            DepthFirstSearch();

            var transpose = CreateTransposeGraph(
                (oldVertex) => new AdjacencyVertex<T>
                {
                    FinalVisitTime = oldVertex.FinalVisitTime,
                    Key = oldVertex.Key,
                    Identifier = oldVertex.Identifier,
                }
            );

            var sortVertexs = transpose._adjacencyDictionary.Values.OrderByDescending(o => o.FinalVisitTime);
            transpose.ResetVertexs(sortVertexs);

            //剩下怎么改造输出了。
            transpose.DepthFirstSearch();

            return stronglyConnectedGraphs;
        }

        #region base

        private int _currentIdentifier = -1;

        private int GetCurrentIdentifier()
        {
            return _currentIdentifier = _currentIdentifier + 1;
        }


        #region graph

        public void CreatGraph(
            IEnumerable<AdjacencyVertex<T>> vertexs
            , IEnumerable<AdjacencyEdge> edges)
        {
            _adjacencyDictionary.Clear();
            foreach (var item in vertexs)
            {
                AddVertex(item);
            }
            foreach (var item in edges)
            {
                AddEdge(item);
            }
        }

        /// <summary>
        /// 创建转置图
        /// </summary>
        /// <param name="copyAction">创建新结点复制属性</param>
        /// <returns></returns>
        public AdjacencyListGraph<T> CreateTransposeGraph(
            Func<AdjacencyVertex<T>, AdjacencyVertex<T>> copyAction = null)
        {
            if (!HasDirection)
            {
                throw new InvalidOperationException("must hasdirection graph");
            }

            if (copyAction == null)
            {
                copyAction = (oldVertex) => new AdjacencyVertex<T>() { Key = oldVertex.Key, Identifier = oldVertex.Identifier };
            }


            var graph = new AdjacencyListGraph<T>(true);

            var vertexs = new List<AdjacencyVertex<T>>();
            var edges = new List<AdjacencyEdge>();

            foreach (var item in _adjacencyDictionary.Values)
            {
                var newVertex = copyAction(item);
                vertexs.Add(newVertex);

                edges.AddRange(GetVertexEdge(item).Select(o => new AdjacencyEdge(o.End, o.Start)));
            }
            graph.CreatGraph(vertexs, edges);
            return graph;
        }

        #endregion

        #region vertex

        /// <summary>
        /// 一般用来改变元素顺序
        /// </summary>
        /// <param name="vertexs"></param>
        private void ResetVertexs(IEnumerable<AdjacencyVertex<T>> vertexs)
        {
            var newAdjacencyDictionary = new Dictionary<int, AdjacencyVertex<T>>();
            foreach (var item in vertexs)
            {
                newAdjacencyDictionary[item.Identifier] = item;
            }
            _adjacencyDictionary = newAdjacencyDictionary;
        }

        public IList<AdjacencyVertex<T>> CreateVertexs(IEnumerable<T> keys)
        {
            var vertexs = new List<AdjacencyVertex<T>>();
            foreach (var key in keys)
            {
                vertexs.Add(CreateVertex(key));
            }
            return vertexs;
        }

        public AdjacencyVertex<T> CreateVertex(T key)
        {
            var vertex = new AdjacencyVertex<T>() { Key = key };
            vertex.Identifier = GetCurrentIdentifier();
            return vertex;
        }


        public IEnumerable<AdjacencyVertex<T>> GetVertexs()
        {
            return _adjacencyDictionary.Values;
        }

        public AdjacencyVertex<T> GetVertex(int id)
        {
            return _adjacencyDictionary[id];
        }

        public AdjacencyVertex<T> GetVertexByKey(int key)
        {
            return _adjacencyDictionary.Values.Where(o => o.Key.CompareTo(key) == 0).FirstOrDefault();
        }

        public AdjacencyVertex<T> AddVertex(T key)
        {
            var vertex = CreateVertex(key);
            _adjacencyDictionary[vertex.Identifier] = vertex;
            return vertex;
        }

        public void AddVertex(AdjacencyVertex<T> vertex)
        {
            _adjacencyDictionary[vertex.Identifier] = vertex;
        }
        #endregion

        #region edge

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

        public AdjacencyEdge CreateEdge(AdjacencyVertex<T> start, AdjacencyVertex<T> end)
        {
            return new AdjacencyEdge(start.Identifier, end.Identifier);
        }

        public void AddEdge(AdjacencyEdge item)
        {
            AddEdgeCore(item.Start, item.End);
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
            if (!_adjacencyDictionary.Keys.Contains(start) || !_adjacencyDictionary.Keys.Contains(end))
            {
                throw new ArgumentOutOfRangeException("can't find vertex");
            }
            var startVertex = _adjacencyDictionary[start];
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

        #endregion

        #endregion
    }
}