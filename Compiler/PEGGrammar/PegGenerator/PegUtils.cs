using Peg.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peg.Generator
{
    public class PegUtils
    {
        public static void Trim(StringBuilder s)
        {
            string sTrimChars = " \t\r\v\n";
            int i, j;
            for (i = 0; i < s.Length; ++i)
            {
                if (sTrimChars.IndexOf(s[i]) == -1) break;
            }
            for (j = s.Length; j > i; j--)
            {
                if (sTrimChars.IndexOf(s[j - 1]) == -1) break;
            }
            s.Remove(j, s.Length - j);
            s.Remove(0, i);
        }
        public static PegNode GetByPath(PegNode node, params int[] path)
        {
            for (int i = 0; i < path.Length; ++i)
            {
                if (node == null || node.id_ != path[i]) return null;
                if (i == path.Length - 1) return node; else node = node.child_;
            }
            return node;
        }

        public static PegNode FindNode(PegNode node, int id, int nodeDistance)
        {
            if (node == null || nodeDistance <= 0) return null;
            if (node.id_ == (int)id) return node;
            PegNode foundNode = FindNode(node.child_, id, nodeDistance - 1);
            if (foundNode != null) return foundNode;
            foundNode = FindNode(node.next_, id, nodeDistance - 1);
            if (foundNode != null) return foundNode;
            return null;
        }
        public static PegNode FindNode(PegNode node, int id)
        {
            return FindNode(node, id, 8);
        }
        public static PegNode FindNode(PegNode node, int nodeDistance, params int[] ids)/*FIND_IN_SEMANTIC_BLOCK*/
        {
            if (node == null || nodeDistance <= 0) return null;
            foreach (int id in ids)
            {
                if (node.id_ == id) return node;
            }
            PegNode foundNode = FindNode(node.child_, nodeDistance - 1, ids);
            if (foundNode != null) return foundNode;
            foundNode = FindNode(node.next_, nodeDistance - 1, ids);
            if (foundNode != null) return foundNode;
            return null;
        }
        public static PegNode FindNode(PegNode node, params int[] ids)/*FIND_IN_SEMANTIC_BLOCK*/
        {
            return FindNode(node, 8, ids);
        }
        public static PegNode FindNodeInParents(PegNode node, int id)
        {
            for (; node != null; node = node.parent_)
            {
                if (node.id_ == (int)id) return node;
            }
            return null;
        }
        public static PegNode FindNodeInParents(PegNode node, params int[] ids)
        {
            foreach (int id in ids)
            {
                PegNode foundNode = FindNodeInParents(node, id);
                if (foundNode != null) return foundNode;
            }
            return null;
        }
        public static PegNode FindNodeNext(PegNode node, int id)
        {
            for (node = node.next_; node != null; node = node.next_)
            {
                if (node.id_ == (int)id) return node;
            }
            return null;
        }

        public static IEnumerable<PegNode> FindNodeGroup(PegNode node, int id)
        {
            var nodes = new List<PegNode>();

            var child = FindNode(node, id);

            if (child != null)
            {
                nodes.Add(child);
            }

            for (child = child.next_; child != null; child = child.next_)
            {
                if (node.id_ == (int)id)
                {
                    nodes.Add(child);
                }
                else
                {
                    break;
                }
            }

            return nodes;

        }

        public static PegNode GetPegSpecification(PegNode root)
        {
            return root.child_.next_;
        }

        public static string GetAsString(string src, PegNode n)
        {
            return n.match_.GetAsString(src);
        }

        public static string GetAsString(string src, PegNode n, params PegNode[] removeNodes)
        {
            var nString = n.match_.GetAsString(src);

            foreach (var removeNode in removeNodes)
            {
                if (n.match_.posBeg_ <= removeNode.match_.posBeg_
                    && n.match_.posEnd_ >= removeNode.match_.posEnd_
                    )
                {

                    nString = nString.Remove(removeNode.match_.posBeg_ - n.match_.posBeg_, removeNode.match_.Length);
                }
                else
                {
                    throw new NotSupportedException("removeNode out index");
                }
            }

            return nString;

        }


        public static string MakeFileName(string sFileTitle, params string[] directories)
        {
            string path = "";
            foreach (string dir in directories)
            {
                path += dir;
                if (dir.Length > 0 && dir[dir.Length - 1] != '\\') path += '\\';
            }
            return path + sFileTitle;
        }
    }
}
