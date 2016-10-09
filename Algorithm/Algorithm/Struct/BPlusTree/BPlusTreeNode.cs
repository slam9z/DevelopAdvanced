using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
	public class BPlusTreeNode<T> where T : IComparable
	{
		public string Identifier { get; set; }

		public bool IsLeaf { get; set; }

		//有会遇到0和1的问题。

		public int KeyCount { get; set; }

		public T[] Keys { get; set; }

		public string[] Children { get; set; }

		//public BPlusTreeNode<T> Parent { get; set; }

		public BPlusTreeNode(int minLimit)
		{
			Keys = new T[2 * minLimit];
			Children = new string[2 * minLimit + 1];
		}

		public T GetKey(int order)
		{
			return Keys[order - 1];
		}

		public void SetKey(int order, T key)
		{
			Keys[order - 1] = key;
		}

		public string GetChild(int order)
		{
			return Children[order - 1];
		}

		public void SetChild(int order, string identifier)
		{
			Children[order - 1] = identifier;
		}

		//不把所有信息ToString都没看了
		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.AppendLine(string.Format("Identifier: {0}  ", Identifier));
			builder.AppendLine(string.Format("KeyCount: {0}  ", KeyCount));
			builder.AppendLine(string.Format("IsLeaf: {0}  ", IsLeaf));

			builder.AppendLine("Keys: ");
			for (int i = 0; i < KeyCount; i++)
			{
				var key = Keys[i];
				builder.Append(string.Format("{0},  ", key));
			}
			builder.AppendLine();

			if (!IsLeaf)
			{
				builder.AppendLine("Children: ");
				for (int i = 0; i < KeyCount + 1; i++)
				{
					var child = Children[i];
					builder.Append(string.Format("{0},  ", child));
				}
				builder.AppendLine();
			}

			return builder.ToString();
		}

	}
}
