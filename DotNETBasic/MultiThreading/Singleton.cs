using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
	public class Singleton
	{
		private static Singleton _current;

		private static object _lockObject;
		private static object GetLockObject()
		{
			if (_lockObject == null)
			{
				return Interlocked.Exchange(ref _lockObject, new Object());
			}
			return _lockObject;
		}

		public static Singleton Current
		{
			get
			{
				if (_current != null)
				{
					return _current;
				}
				lock(GetLockObject())
				{
					return _current = new Singleton();
				}
			}
		}

		private Singleton()
		{

		}
	}
}
