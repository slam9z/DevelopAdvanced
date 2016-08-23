using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public class LinkList<T> where T : IEquatable<T>
	{
		private LinkNode<T> _head;

		public bool IsEmpty
		{
			get
			{
				return _head == null;
			}
		}

		public LinkNode<T> Search(T data)
		{
			var node = _head;
			while (node != null && node.Data.Equals(data))
			{
				node = node.Next;
			}
			return node;
		}

		//将新数据插入到最前端
		public LinkNode<T> Insert(T data)
		{
			var newNode = new LinkNode<T>() { Data = data };
			Insert(newNode);
			return newNode;
		}

		public void Insert(LinkNode<T> node)
		{
			node.Next = _head;
			if (_head != null)
			{
				_head.Prev = node;
			}
			_head = node;
		}


		public void Delete(LinkNode<T> node)
		{
			//prev
			if (node.Prev != null)
			{
				node.Prev.Next = node.Next;
			}
			else
			{
				_head = node.Next;
			}

			//next
			if (node.Next != null)
			{
				node.Next.Prev = node.Prev;
			}

		}



	}
}
