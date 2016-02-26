using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MultiThreading
{
	public class ImageCache
	{
		private object _syncObject = new object();

		private Dictionary<string, string> _imageDic = new Dictionary<string, string>();

		public string GetImage(string path)
		{
			string image = null;
			Monitor.Enter(_syncObject);
			image = GetImageInner(path);
			Monitor.Exit(_syncObject);
			return image;
			//lock (_syncObject)
			//{
			//return	GetImageInner(path);
			//}
		}

		public void AddImage(string path, string image)
		{
			lock (_syncObject)
			{
				AddImageInner(path, image);
			}
		}


		public string GetImageInner(string path)
		{
			string image = null;
			if (_imageDic.ContainsKey(path))
			{
				image = _imageDic[path];
			}
			else
				image = string.Empty;
			Console.WriteLine("GetImage path: {0},image: {1} ,threadId: {2}", path, image, Thread.CurrentThread.ManagedThreadId);
			return image;
		}

		public void AddImageInner(string path, string image)
		{
			_imageDic[path] = image;

			Console.WriteLine("AddImage path: {0},image: {1} ,threadId: {2}", path, image, Thread.CurrentThread.ManagedThreadId);
		}
	}
}
