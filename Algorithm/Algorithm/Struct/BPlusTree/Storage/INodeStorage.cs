using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public interface INodeStorage<T> where T : IComparable
	{
		BPlusTreeNode<T> Read(string identifier);

		void Write(BPlusTreeNode<T> node);

		void Delete(string identifier);


	}
}
