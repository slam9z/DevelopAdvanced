using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public class Queue<T>
	{
		private IList<T> _datas = new List<T>();

		private int _head;

		private int _tail;

		public bool IsEmpty
		{
			get
			{
				return _head ==_tail;
			}
		}

		//问题多多，这两种操作还是用链表操作最好
		//反复使用会爆炸。
		public void Enqueue(T data)
		{
			if (_tail >= _datas.Count)
			{
				_datas.Add(data);
			}
			else
			{
				_datas[_tail] = data;
			}
			_tail++;	
		}

		public T Dequeue()
		{
			if (_head == _tail)
			{
				throw new InvalidOperationException("queue is empty");
			}

			var data= _datas[_head];
			_head++;
			return data;
		}
	}
}
