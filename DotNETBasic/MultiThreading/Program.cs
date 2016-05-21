using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading
{
	class Program
	{
		static void Main(string[] args)
		{
			ImageCacheTest();
			Console.ReadKey();
		}


		public static void ImageCacheTest()
		{
			var max = 10;
			var cache = new ImageCache();
			Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Start Task1");
				for (int i = 0; i < max; i = i + 2)
				{
					cache.AddImage(i.ToString(), (i * 100).ToString());
				}
			});

			Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Start Task2");
				for (int i = 0; i < max; i = i + 2)
				{
					cache.GetImage(i.ToString());
				}
			});


			Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Start Task3");
				for (int i = max; i < 2 * max; i = i + 2)
				{
					cache.AddImageInner(i.ToString(), (i * 100).ToString());
				}
			});

			Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Start Task4");
				for (int i = max; i < 2 * max; i = i + 2)
				{
					cache.GetImageInner(i.ToString());
				}
			});
		}

	}
}
