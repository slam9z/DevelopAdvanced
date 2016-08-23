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
	public class LinkListTests
	{
		[TestMethod()]
		public void InsertAndDeleteTest()
		{
			var link = new LinkList<int>();

			var node1 = link.Insert(1);
			var node2 = link.Insert(2);
			var node3 = link.Insert(3);
			var node4 = link.Insert(4);

			link.Delete(node2);

			//不明觉厉
			Assert.AreSame(node1.Prev, node3);
			Assert.AreSame(node3.Next, node1);

			link.Delete(node3);
			link.Delete(node4);
			link.Delete(node1);

			Assert.IsTrue(link.IsEmpty);

		}
	}
}