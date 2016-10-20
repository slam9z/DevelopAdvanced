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
	public class QueueTests
	{
		[TestMethod()]
		public void EnqueueAndDequeueTest()
		{
			var queue = new Queue<int>();
			queue.Enqueue(1);
			queue.Enqueue(5);
			queue.Enqueue(3);
			var data = queue.Dequeue();
			Assert.AreEqual(1, data);

			data = queue.Dequeue();
			Assert.AreEqual(5, data);

			Assert.IsFalse(queue.IsEmpty);

			data = queue.Dequeue();
			Assert.AreEqual(3, data);

			Assert.IsTrue(queue.IsEmpty);

		}
	}
}