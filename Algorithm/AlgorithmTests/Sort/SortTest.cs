using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort.Tests
{
	[TestClass()]
	public class SortTest
	{
		

		[TestMethod()]
		public void BubbleSortTest()
		{
			var sort = new BubbleSort();
			RunTest1(sort);
		}


		private IList<int> testData1 = new List<int>() { 12, 3, 8, 19, 5, 1, 2, 9, 3 };

		private void RunTest1(ISort sort)
		{
			RunTest(sort, testData1);
		}


		private void VerifyTestResult(IList<int> source, IList<int> result)
		{
			var sortData = source.OrderBy(o => o).ToList();

			Assert.AreEqual(sortData.Count, result.Count, "结果数不想等");

			for (int i = 0; i < sortData.Count; i++)
			{
				Assert.AreEqual(sortData[i], result[i], "排序数值不想等");
			}
		}

		private void RunTest(ISort sort, IList<int> source)
		{
			var result = sort.Sort(source);
			VerifyTestResult(source, result);
		}
	}
}