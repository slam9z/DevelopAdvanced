using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	//没事不要叹气

	public class LinkNode<T> 
	{
		public LinkNode<T> Prev { get; set; }

		public LinkNode<T> Next { get; set; }

		public T Data { get; set; }
	}
}
