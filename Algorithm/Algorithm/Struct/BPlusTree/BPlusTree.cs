using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public class BPlusTree<T> where T : IComparable
	{
		private INodeStorage<T> _storage = new DictionaryNodeStorage<T>();

		public BPlusTreeNode<T> Root { get; private set; }

		public int MinLimit { get; set; } = 4;


		public BPlusTreeNode<T> Search(BPlusTreeNode<T> searchedNode, T key)
		{
			var pointer = 1;

			while (pointer <= searchedNode.KeyCount && key.CompareTo(searchedNode.GetKey(pointer)) <= 0)
			{
				pointer++;
			}

			if (pointer <= searchedNode.KeyCount && key.CompareTo(searchedNode.GetKey(pointer)) == 0)
			{
				return _storage.Read(searchedNode.GetChild(pointer));
			}

			if (searchedNode.IsLeaf)
			{
				return null;
			}
			else
			{
				var childNode = _storage.Read(searchedNode.GetChild(pointer));
				return Search(childNode, key);
			}
		}

		public void Create()
		{
			var node = CreateBPlusTreeNode();
			node.IsLeaf = true;
			node.KeyCount = 0;

			_storage.Write(node);
			Root = node;
		}


		#region Insert

		public void Insert(T key)
		{
			if (Root == null)
			{
				Create();
			}
			var root = Root;
			if (root.KeyCount == 2 * MinLimit - 1)
			{
				var newRoot = CreateBPlusTreeNode();
				Root = newRoot;
				newRoot.IsLeaf = false;
				newRoot.KeyCount = 0;
				newRoot.SetChild(1, root.Identifier);
				SplitChild(newRoot, 1, root);
				InsertNotFull(newRoot, key);
			}
			else
			{
				InsertNotFull(root, key);
			}
		}

		private void InsertNotFull(BPlusTreeNode<T> insertedNode, T key)
		{
			var pointer = insertedNode.KeyCount;

			if (insertedNode.IsLeaf)
			{
				while (pointer >= 1 && key.CompareTo(insertedNode.GetKey(pointer)) < 0)
				{
					insertedNode.SetKey(pointer + 1, insertedNode.GetKey(pointer));
					pointer--;
				}
				//pointer+1？
				insertedNode.SetKey(pointer + 1, key);
				insertedNode.KeyCount = insertedNode.KeyCount + 1;
				_storage.Write(insertedNode);
			}
			else
			{
				while (pointer >= 1 && key.CompareTo(insertedNode.GetKey(pointer)) < 0)
				{
					insertedNode.SetKey(pointer + 1, insertedNode.GetKey(pointer));
					pointer--;
				}
				pointer++;

				var child = _storage.Read(insertedNode.GetChild(pointer));

				if (child.KeyCount == 2 * MinLimit - 1)
				{
					//pointer?
					SplitChild(insertedNode, pointer, child);

					//pointer;
					if (key.CompareTo(insertedNode.GetKey(pointer)) > 0)
					{
						pointer++;
					}
				}
				else
				{
					InsertNotFull(child, key);
				}
			}
		}

		private void SplitChild(BPlusTreeNode<T> parentNode, int splitedPointer, BPlusTreeNode<T> splitedNode)
		{
			#region //newSplitedNode

			var newSplitedNode = CreateBPlusTreeNode();
			newSplitedNode.IsLeaf = splitedNode.IsLeaf;
			newSplitedNode.KeyCount = MinLimit - 1;

			for (int i = 1; i <= MinLimit - 1; i++)
			{
				newSplitedNode.SetKey(i, splitedNode.GetKey(i + MinLimit));
			}
			if (!splitedNode.IsLeaf)
			{
				for (int i = 1; i <= MinLimit; i++)
				{
					newSplitedNode.SetChild(i, splitedNode.GetChild(i + MinLimit));
				}
			}

			#endregion


			#region //splitedNode

			splitedNode.KeyCount = MinLimit - 1;

			#endregion


			#region //parentNode

			for (int i = parentNode.KeyCount + 1; i >= splitedPointer + 1; i--)
			{
				parentNode.SetChild(i + 1, parentNode.GetChild(i));
			}

			parentNode.SetChild(splitedPointer + 1, newSplitedNode.Identifier);

			for (int i = parentNode.KeyCount; i >= splitedPointer + 1; i--)
			{
				parentNode.SetKey(i + 1, parentNode.GetKey(i));
			}

			parentNode.SetKey(splitedPointer, splitedNode.GetKey(MinLimit));

			parentNode.KeyCount = parentNode.KeyCount + 1;

			#endregion

			_storage.Write(splitedNode);
			_storage.Write(newSplitedNode);
			_storage.Write(parentNode);
		}



		#endregion


		#region Delete

		public void Delete()
		{

		}

		#endregion


		#region helper

		private int _nodeCount;

		public string CreateIdentifier()
		{
			return _nodeCount.ToString("N");
		}

		private BPlusTreeNode<T> CreateBPlusTreeNode()
		{
			var node = new BPlusTreeNode<T>(MinLimit);
			_nodeCount++;
			node.Identifier = CreateIdentifier();
			return node;
		}

		#endregion
	}
}
