using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public class Stack<T>
	{
		//用List适合吗？不用用什么呢？

		private IList<T> _source;

		public int Top { get; private set; }

		public bool IsEmpty
		{
			get
			{
				return Top < 0;
			}
		}

		
		public Stack()
		{
			_source = new List<T>(4);
			Top = -1;
		}


		//线程不安全
		public void Push(T data)
		{
			Top = Top + 1;
			if (Top >= _source.Count)
			{
				_source.Add(data);
			}
			else
			{
				_source[Top] = data;
			}
			
		}

		public T Pop()
		{
			if (IsEmpty)
			{
				throw new InvalidOperationException("underflow");
			}
			Top = Top - 1;
			return _source[Top + 1];
		}
	}
}
