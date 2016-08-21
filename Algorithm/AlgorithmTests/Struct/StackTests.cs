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
	public class StackTests
	{
		[TestMethod()]
		public void PushTest()
		{
			var stack = new Stack<int>();
			stack.Push(1);
			stack.Push(5);
			stack.Push(3);
			stack.Push(23);
			stack.Push(123);
		}

		[TestMethod()]
		[ExpectedException(exceptionType: typeof(InvalidOperationException))]
		public void PopEmptyTest()
		{
			var stack = new Stack<int>();
			stack.Pop();

		}

		[TestMethod()]
		public void PushAndPopTest()
		{
			var stack = new Stack<int>();
			stack.Push(1);
			stack.Push(5);
			stack.Push(3);
			var pop = stack.Pop();
			Assert.AreEqual(3, pop);

			pop = stack.Pop();
			Assert.AreEqual(5, pop);

			Assert.IsFalse(stack.IsEmpty);

			pop = stack.Pop();
			Assert.AreEqual(1, pop);

			Assert.IsTrue(stack.IsEmpty);
		}


	}
}