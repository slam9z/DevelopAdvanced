using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Algorithm.Struct.Tests
{
	[TestClass()]
	public class AdjacencyListGraphTestss
	{
		private AdjacencyListGraph<int> CreateGraph1()
		{
			var  vertexs=new List<int>
			{
			    1,2,3,4,5	
			};
			
			var edges=new List<Tuple<int,int>>
			{
				new Tuple<int,int>(0,1),
				new Tuple<int,int>(0,4),
				
				new Tuple<int,int>(1,2),
				new Tuple<int,int>(1,3),
				new Tuple<int,int>(1,4),
				
				new Tuple<int,int>(2,3),
				new Tuple<int,int>(3,4),
			};
			
			var graph=new AdjacencyListGraph<int>();
			
			foreach (var item in vertexs)
			{
				graph.AddVertex(item);
			}
			
			foreach (var item in edges)
			{
				graph.AddEdge(item.Item1,item.Item2);
			}
			
			return graph;
		}

		[TestMethod()]
		public void BreadthFirstSearchTest()
		{
			var graph=CreateGraph1();
			
			Console.WriteLine("start");
			
			var source=graph.GetVertexByIndex(0);
			Console.WriteLine(source);
			graph.BreadthFirstSearch(source,
				(vertex)=>
				{
					Console.Write("{0} ,",vertex.Key);
				}
			);
		}


	}
}