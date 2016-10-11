﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmTests
{
	public static class TestHepler
	{
		public static IList<int> GetRandomList()
		{
			var random = new Random();
			var length = random.Next(1, 1000);
			var datas = new List<int>(length);
			for (int i = 0; i < length; i++)
			{
				datas.Add(random.Next(1, 1000));
			}
			return datas;
		}


		public static IList<T> GetRandomList<T>(IList<T> source)
		{
			var random = new Random();

			var count = random.Next(1, source.Count / 2);

			for (int i = 1; i <= count; i++)
			{
				var exchange = random.Next(0, source.Count - 1);
				var temp = source[exchange];
				source[exchange] = source[source.Count - 1 - exchange];
				source[source.Count - 1 - exchange] = temp;
			}
			return source;
		}

		public static void PrintList<T>(IEnumerable<T> datas, string message = default(string))
		{
			Console.WriteLine("{0}", message);
			foreach (var data in datas)
			{
				Console.Write("{0}, ", data);
			}
			Console.WriteLine("");
		}

		public static void PrintListWithOrder<T>(IEnumerable<T> datas, string message = default(string))
		{
			datas = datas.OrderBy(t => t);

			Console.WriteLine("{0}", message);
			foreach (var data in datas)
			{
				Console.Write("{0}, ", data);
			}
			Console.WriteLine("");
		}
	}
}
