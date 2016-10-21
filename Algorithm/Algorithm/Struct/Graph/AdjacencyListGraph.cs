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

        private IList<AdjacencyEdge<T>> _allEdges = new List<AdjacencyEdge<T>>();

        public readonly bool HasDirection;

        public AdjacencyListGraph()
        {

        }

        public AdjacencyListGraph(bool hasDirection)
        {
            HasDirection = hasDirection;
        }



        #region   //BFS  最慢的就是你

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
                    var endVertex = edge.End;
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
                var vertex = item.End;
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

        #region StronglyConnectedComponenets

        /// <summary>
        /// 输出有点不明啊
        /// </summary>
        public IList<AdjacencyListGraph<T>> GetStronglyConnectedComponenets()
        {

            DepthFirstSearch();

            //方便查看
            var graph = this;

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
            var stronglyConnectedGraphs = transpose.DepthFirstSearchStronglyConnected();

            return stronglyConnectedGraphs;
        }


        //这个用改造通用版本有点勉强吧！应该没错
        private IList<AdjacencyListGraph<T>> DepthFirstSearchStronglyConnected()
        {
            var stronglyConnectedGraphs = new List<AdjacencyListGraph<T>>();

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
                    var graph = new AdjacencyListGraph<T>(true);

                    DepthFirstSearchVisitStronglyConnected(item, graph);

                    stronglyConnectedGraphs.Add(graph);
                }
            }
            return stronglyConnectedGraphs;

        }



        private void DepthFirstSearchVisitStronglyConnected
            (AdjacencyVertex<T> source
            , AdjacencyListGraph<T> graph
            )
        {

            source.Color = Color.Gray;
            _time = _time + 1;
            source.FisrtVisitTime = _time;

            var edges = GetVertexEdge(source);

            //调整
            var newSource = CreateVertex(source);
            graph.AddVertex(newSource);

            foreach (var item in edges)
            {
                var vertex = item.End;

                if (vertex.Color == Color.White)
                {
                    //调整

                    var newVertex = CreateVertex(vertex);
                    graph.AddVertex(newVertex);

                    graph.AddEdge(new AdjacencyEdge<T>(newVertex, newSource));

                    vertex.Parent = source;
                    DepthFirstSearchVisitStronglyConnected(vertex, graph);
                }
            }

            source.Color = Color.Black;

            _time = _time + 1;
            source.FinalVisitTime = _time;

        }
        #endregion

        #region base

        private int _currentIdentifier = -1;

        private int GetCurrentIdentifier()
        {
            return _currentIdentifier = _currentIdentifier + 1;
        }


        #region graph

        public void CreatGraph(
            IEnumerable<AdjacencyVertex<T>> vertexs
            , IEnumerable<AdjacencyEdge<T>> edges)
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

            var vertexs = new Dictionary<int, AdjacencyVertex<T>>();
            var edges = new List<AdjacencyEdge<T>>();

            foreach (var item in _adjacencyDictionary.Values)
            {
                var newVertex = copyAction(item);
                vertexs.Add(newVertex.Identifier, newVertex);

            }

            foreach (var item in _adjacencyDictionary.Values)
            {
                edges.AddRange(GetVertexEdge(item)
                    .Select
                    (o => new AdjacencyEdge<T>(vertexs[o.End.Identifier], vertexs[o.Start.Identifier]))
                    );
            }

            graph.CreatGraph(vertexs.Values, edges);
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

        public AdjacencyVertex<T> CreateVertex(AdjacencyVertex<T> vertex)
        {
            return new AdjacencyVertex<T>() { Key = vertex.Key, Identifier = vertex.Identifier };
        }

        public bool IsVertexExist(AdjacencyVertex<T> vertex)
        {
            return _adjacencyDictionary.ContainsKey(vertex.Identifier);
        }

        public IEnumerable<AdjacencyVertex<T>> GetVertexs()
        {
            return _adjacencyDictionary.Values;
        }


        public AdjacencyVertex<T> GetVertexByKey(int key)
        {
            return _adjacencyDictionary.Values.Where(o => o.Key.CompareTo(key) == 0).FirstOrDefault();
        }

        public AdjacencyVertex<T> AddVertex(T key)
        {
            var vertex = CreateVertex(key);
            AddVertex(vertex);
            return vertex;
        }

        public void AddVertex(AdjacencyVertex<T> vertex)
        {
            if (!IsVertexExist(vertex))
            {
                _adjacencyDictionary[vertex.Identifier] = vertex;
            }
            else
            {

            }
        }
        #endregion

        #region edge

        public IEnumerable<AdjacencyEdge<T>> GetVertexEdge(AdjacencyVertex<T> vertex)
        {
            var edges = new List<AdjacencyEdge<T>>();
            var edge = vertex.FirstEdge;
            while (edge != null)
            {
                edges.Add(edge);
                edge = edge.Next;
            }
            return edges;
        }

        public  IEnumerable<AdjacencyEdge<T>> GetEdges()
        {
            return _allEdges;
        }
 
        public AdjacencyEdge<T> CreateEdge(AdjacencyVertex<T> start, AdjacencyVertex<T> end)
        {
            return new AdjacencyEdge<T>(start, end);
        }

        public void AddEdge(AdjacencyEdge<T> item)
        {
            AddEdgeCore(item.Start, item.End);
        }


        public void AddEdge(AdjacencyVertex<T> first, AdjacencyVertex<T> second)
        {
            AddEdgeCore(first, second);
            if (!HasDirection)
            {
                AddEdgeCore(second, first);
            }
        }

        public void AddEdgeCore(AdjacencyVertex<T> start, AdjacencyVertex<T> end)
        {
            

            var startVertex = start;
            var edge = new AdjacencyEdge<T>(start, end);

            _allEdges.Add(edge);

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