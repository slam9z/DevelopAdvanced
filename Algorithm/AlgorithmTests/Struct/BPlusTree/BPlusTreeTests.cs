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
	public class BPlusTreeTests
	{
		#region data1 and  result

		private IList<int> TestData1 = new List<int>
		{
			1,2,3,4,5,
			6,7,8,9,10,
			11,12,13,14,15,
		};

		//limit 4

		//			4,8  keys id 2
		//			1 3 4   childs

		//id 1		id 3      id 4
		//1,2,3      5,6,7       9到15 keys

		//但是有多余 但是有多余
		//的数据1到7 多余的数据5到11

		//结果是符合算法的。

		#endregion



		[TestMethod()]
		public void InsertTest()
		{
			var bTree = new BPlusTree<int>();

			foreach (var item in TestData1)
			{
				bTree.Insert(item);
			}
		}

		[TestMethod()]
		public void SearchTest()
		{
			var bTree = new BPlusTree<int>();

			foreach (var item in TestData1)
			{
				bTree.Insert(item);
			}

			foreach (var item in TestData1)
			{
				var node = bTree.Search(bTree.Root, item);

				//节点的值呢 ?
				Console.WriteLine("Search key: {0}",item);
				Console.WriteLine(node);

				Assert.IsNotNull(node);
				Assert.IsTrue(node.Keys.Contains(item));
			}
		}
	}
}