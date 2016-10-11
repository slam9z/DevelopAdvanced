using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public class DictionaryNodeStorage<T> : INodeStorage<T> where T : IComparable
	{
		private Dictionary<string, BPlusTreeNode<T>> _repository = new Dictionary<string, BPlusTreeNode<T>>();

        public void Delete(BPlusTreeNode<T> node)
        {
            Delete(node.Identifier);
        }

        public void Delete(string identifier)
		{
			if (_repository.ContainsKey(identifier))
			{
				_repository.Remove(identifier);
			}
		}

		public BPlusTreeNode<T> Read(string identifier)
		{
            if (identifier == null)
            {
                return null;
            }
            if (_repository.ContainsKey(identifier))
            {
                return _repository[identifier];
            }
            return null;
		}

		public void Write(BPlusTreeNode<T> node)
		{
			_repository[node.Identifier] = node;
		}
	}
}
