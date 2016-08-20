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
			RunAllTest(sort);
		}

		[TestMethod()]
		public void InsertSortTest()
		{
			var sort = new InsertSort();
			RunAllTest(sort);
		}

		[TestMethod()]
		public void MergeSortTest()
		{
			var sort = new MergeSort();
			//Test1(sort);
			RunAllTest(sort);
		}


		[TestMethod()]
		public void HeapSortTest()
		{
			var sort = new HeapSort();
			//Test1(sort);
			RunAllTest(sort);
		}


		private void RunAllTest(ISort sort)
		{
			EmptyTest(sort);
			OneDataTest(sort);
			Test1(sort);

			for (int i = 0; i < 100; i++)
			{
				RandomDataTest(sort);
			}

		}

		private IList<int> testData1 = new List<int>() { 12, 3, 8, 19, 5, 1, 2, 9, 3 };

		private void Test1(ISort sort)
		{
			RunTest(sort, testData1);
		}

		private void EmptyTest(ISort sort)
		{
			RunTest(sort, new List<int>());
		}

		private void OneDataTest(ISort sort)
		{
			RunTest(sort, new List<int>() { 1 });
		}

		private void RandomDataTest(ISort sort)
		{
			var random = new Random();
			var length = random.Next(1, 1000);
			var data = new List<int>(length);
			for (int i = 0; i < length; i++)
			{
				data.Add(random.Next(1, 1000));
			}
			RunTest(sort, data);
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