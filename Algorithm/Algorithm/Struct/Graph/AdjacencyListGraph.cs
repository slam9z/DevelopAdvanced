using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Algorithm.Struct
{
	

public class AdjacencyVertex<T>
{
	public T Key{get;set;}
	
	public AdjacencyEdge FirstEdge{get;set;}
	
	//某些算法用到,不属于自身性质
	public AdjacencyVertex<T> Parent{get;set;}
	
	public int Distance{get;set;}
}

internal enum Color
{
	White,
	Gray,
	Black,
	
}
public class AdjacencyEdge
{
	public int Start {get;set;}
	public int End {get;set;}
	
	public AdjacencyEdge Next{get;set;}
	
	public AdjacencyEdge() 
	{
		
	}
	
	public AdjacencyEdge(int start,int end) 
	{
		Start=start;
		End=end;
	}
}

public class AdjacencyListGraph<T>: GraphBase<T>
{
	private IList<AdjacencyVertex<T>> _adjacencyList=new List<AdjacencyVertex<T>>();
	
	//public readonly bool HasDirection{get;set;}
	public  bool HasDirection{get;set;}
	
    public AdjacencyListGraph()
	{
		
	}
    public AdjacencyListGraph(bool hasDirection)
	{
		HasDirection=hasDirection;
	}
	
	public override void BreadthFirstSearch(AdjacencyVertex<T> source)
	{
		var vertexcount=_adjacencyList.Count;
		var sourceIndex=_adjacencyList.IndexOf(source);
		
		var colors=new List<Color>(vertexcount);
		var grayQueue=new Queue<AdjacencyVertex<T>>();
		
	    for (int i = 0; i < vertexcount; i++)
		{
			var vertex=_adjacencyList[i];
			if(vertex==source)
			{
				continue;
			}
			vertex.Distance=0;
			vertex.Parent=null;
			colors[i]=Color.White;
		}
		
		colors[sourceIndex]=Color.Gray;
		source.Parent=null;
		source.Distance=0;
		
		grayQueue.Enqueue(source);
		while(!grayQueue.IsEmpty)
		{
			var startVertex=grayQueue.Dequeue();
			foreach (var edge in GetVertexEdge(startVertex))
			{
				var endeEnd=edge.End;
				var endVertex=_adjacencyList[endeEnd];
				if(colors[endeEnd]==Color.White)
				{
					colors[endeEnd]=Color.Gray;
					endVertex.Distance=startVertex.Distance+1;
					endVertex.Parent=startVertex;
					grayQueue.Enqueue(endVertex);
				}
			}
			colors[_adjacencyList.IndexOf(startVertex)]=Color.Black;
		}
	}
	
	public IEnumerable<AdjacencyEdge> GetVertexEdge(AdjacencyVertex<T> vertex)
	{
		var edges=new List<AdjacencyEdge>();
		var edge=vertex.FirstEdge;
		while(edge!=null)
		{
			edges.Add(edge);
			edge=edge.Next;
		}
		return edges;
	}
	
	public int AddVertex(T key)
	{
		var vertex=new AdjacencyVertex<T>(){Key=key};
		_adjacencyList.Add(vertex);
		return _adjacencyList.Count;		
	}
	
	public void AddEdge(int first, int second)
	{
		AddEdgeCore(first,second);
		if(HasDirection)
		{
			AddEdgeCore(second,first);
		}
	}
	
	public void AddEdgeCore(int start, int end)
	{
		if(start>_adjacencyList.Count||end>_adjacencyList.Count)
		{
			throw new ArgumentOutOfRangeException("can't find vertex");
		}
		var startVertex=_adjacencyList[start];
		var edge=new AdjacencyEdge(start,end);
		
		if(startVertex.FirstEdge==null)
		{
			startVertex.FirstEdge=edge;
		}
		else
		{
			var pointerEdge=startVertex.FirstEdge;
			while(pointerEdge.Next!=null)
			{
				pointerEdge=pointerEdge.Next;
			}
			pointerEdge=edge;
		}
	}
	
}
}