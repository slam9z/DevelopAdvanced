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

            while (pointer <= searchedNode.KeyCount && key.CompareTo(searchedNode.GetKey(pointer)) > 0)
            {
                pointer++;
            }

            if (pointer <= searchedNode.KeyCount && key.CompareTo(searchedNode.GetKey(pointer)) == 0)
            {
                return searchedNode;
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

        public void Order(BPlusTreeNode<T> node, Action<T> action)
        {
            for (int i = 1; i <= node.KeyCount; i++)
            {
                if (!node.IsLeaf)
                {
                    var child = _storage.Read(node.GetChild(i));
                    if (child != null)
                    {
                        Order(child, action);
                    }
                }
                action(node.GetKey(i));
            }
            if (!node.IsLeaf)
            {
                var child = _storage.Read(node.GetChild(node.KeyCount + 1));
                if (child != null)
                {
                    Order(child, action);
                }
            }
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

                    child = _storage.Read(insertedNode.GetChild(pointer));
                }

                InsertNotFull(child, key);
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

            for (int i = parentNode.KeyCount; i >= splitedPointer; i--)
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

        public void Delete(T key)
        {
            DeleteCore(Root, key);
        }

        //没有伪代码实现起来问题多多。IsLeaf值问题，是否删除节点存储的问题。
        private void DeleteCore(BPlusTreeNode<T> node, T key)
        {
            var keyIndex = ArrayIndex(node.Keys, node.KeyCount, key);

            #region key在node节点内

            if (keyIndex != -1)
            {
                var pointer = keyIndex + 1;

                if (node.IsLeaf)
                {
                    ArrayRemove(node.Keys, node.KeyCount, key);
                    node.KeyCount = node.KeyCount - 1;

                    if (node.KeyCount == 0)
                    {
                        _storage.Delete(node.Identifier);
                    }
                    else
                    {
                        _storage.Write(node);
                    }
                    return;
                }

                else
                {
                    var preChild = _storage.Read(node.GetChild(pointer));
                    if (preChild.KeyCount >= MinLimit)
                    {
                        var preKey = preChild.GetKey(preChild.KeyCount);
                        node.SetKey(pointer + 1, preKey);
                        DeleteCore(preChild, preKey);

                        _storage.Write(node);
                        return;
                    }

                    var postChild = _storage.Read(node.GetChild(pointer + 1));
                    if (postChild.KeyCount >= MinLimit)
                    {
                        var postKey = postChild.GetKey(postChild.KeyCount);
                        node.SetKey(pointer + 1, postKey);
                        DeleteCore(postChild, postKey);

                        _storage.Write(node);
                        return;
                    }
                    //all minlimint-1 merge  preChild key postChild to preChild
                    ArrayRemove(node.Keys, node.KeyCount, key);
                    ArrayRemove(node.Children, node.KeyCount + 1, node.GetChild(pointer + 1));
                    node.KeyCount = node.KeyCount - 1;

                    preChild = Merge(preChild, key, postChild);
                    _storage.Delete(postChild);

                    _storage.Write(preChild);
                    _storage.Write(node);

                    DeleteCore(preChild, key);

                    return;
                }
            }

            #endregion

            #region key不在node节点内

            else
            {
                var pointer = node.KeyCount;
                while (pointer >= 1 && key.CompareTo(node.GetKey(pointer)) < 0)
                {
                    pointer--;
                }
                if (pointer != node.KeyCount || pointer == 0)
                {
                    pointer++;
                }

                var targetRoot = _storage.Read(node.GetChild(pointer));

                //删除不存在的值
                if (targetRoot == null)
                {
                    return;
                }

                if (targetRoot.KeyCount >= MinLimit)
                {
                    DeleteCore(targetRoot, key);
                    return;
                }

                #region 从兄弟获取位置，然后再删除

                BPlusTreeNode<T> rootPreBrother = null;
                if (pointer > 1)
                {
                    rootPreBrother = _storage.Read(node.GetChild(pointer - 1));
                    if (rootPreBrother.KeyCount >= MinLimit)
                    {
                        targetRoot.KeyCount = targetRoot.KeyCount + 1;
                        targetRoot.SetKey(targetRoot.KeyCount, node.GetKey(pointer));
                        targetRoot.SetChild(targetRoot.KeyCount + 1, rootPreBrother.GetChild(rootPreBrother.KeyCount));

                        node.SetKey(pointer, rootPreBrother.GetKey(rootPreBrother.KeyCount));


                        ArrayRemove(rootPreBrother.Keys, rootPreBrother.KeyCount, rootPreBrother.GetKey(rootPreBrother.KeyCount));
                        if (!rootPreBrother.IsLeaf)
                        {
                            ArrayRemove(rootPreBrother.Children, rootPreBrother.KeyCount + 1, rootPreBrother.GetChild(rootPreBrother.KeyCount));
                        }
                        rootPreBrother.KeyCount = rootPreBrother.KeyCount - 1;

                        _storage.Write(targetRoot);
                        _storage.Write(node);
                        _storage.Write(rootPreBrother);

                        DeleteCore(targetRoot, key);
                        return;
                    }

                }

                BPlusTreeNode<T> rootPostBrother = null;

                if (pointer != node.KeyCount + 1)
                {
                    rootPostBrother = _storage.Read(node.GetChild(pointer + 1));
                    if (rootPostBrother.KeyCount >= MinLimit)
                    {
                        targetRoot.KeyCount = targetRoot.KeyCount + 1;
                        targetRoot.SetKey(targetRoot.KeyCount, node.GetKey(pointer));
                        targetRoot.SetChild(targetRoot.KeyCount + 1, rootPostBrother.GetChild(1));

                        node.SetKey(pointer, rootPostBrother.GetKey(1));


                        ArrayRemove(rootPostBrother.Keys, rootPostBrother.KeyCount, rootPostBrother.GetKey(1));
                        if (!rootPostBrother.IsLeaf)
                        {
                            ArrayRemove(rootPostBrother.Children, rootPostBrother.KeyCount + 1, rootPostBrother.GetChild(1));
                        }
                        rootPostBrother.KeyCount = rootPostBrother.KeyCount - 1;

                        _storage.Write(targetRoot);
                        _storage.Write(node);
                        _storage.Write(rootPostBrother);

                        DeleteCore(targetRoot, key);
                        return;
                    }
                }

                #endregion

                #region 和兄弟接到合并

                if (rootPreBrother != null)
                {
                    var nodeKey = node.GetKey(pointer);
                    ArrayRemove(node.Keys, node.KeyCount, nodeKey);
                    if (!node.IsLeaf)
                    {
                        ArrayRemove(node.Children, node.KeyCount + 1, node.GetChild(pointer));
                    }
                    node.KeyCount = node.KeyCount - 1;

                    var merge = Merge(rootPreBrother, node.GetKey(pointer), targetRoot);
                    _storage.Delete(targetRoot);

                    if (node == Root && node.KeyCount == 0)
                    {
                        Root = merge;
                        _storage.Delete(node);
                    }

                    DeleteCore(merge, key);

                    _storage.Write(rootPreBrother);
                    return;
                }

                if (rootPostBrother != null)
                {
                    var nodeKey = node.GetKey(pointer);
                    ArrayRemove(node.Keys, node.KeyCount, nodeKey);
                    if (!node.IsLeaf)
                    {
                        ArrayRemove(node.Children, node.KeyCount + 1, node.GetChild(pointer + 1));
                    }
                    node.KeyCount = node.KeyCount - 1;

                    var merge = Merge(targetRoot, nodeKey, rootPostBrother);
                    _storage.Delete(rootPostBrother);

                    if (node == Root && node.KeyCount == 0)
                    {
                        Root = merge;
                        _storage.Delete(node);
                    }

                    DeleteCore(merge, key);

                    _storage.Write(merge);
                    return;
                }

                #endregion

            }

            #endregion
        }

        private BPlusTreeNode<T> Merge(BPlusTreeNode<T> preSource, T key, BPlusTreeNode<T> postSource)
        {
            preSource.SetKey(preSource.KeyCount + 1, key);

            for (int i = 1; i <= postSource.KeyCount; i++)
            {
                preSource.SetKey(preSource.KeyCount + 1 + i, postSource.GetKey(i));
            }

            if (!preSource.IsLeaf)
            {
                for (int i = 1; i <= postSource.KeyCount + 1; i++)
                {
                    preSource.SetChild(preSource.KeyCount + 1 + i, postSource.GetChild(i));
                }
            }

            preSource.KeyCount = preSource.KeyCount + 1 + postSource.KeyCount;
            return preSource;

        }
        private int ArrayIndex<R>(R[] array, int length, R key) where R : IComparable
        {
            var index = -1;

            for (int i = 0; i < length; i++)
            {
                if (key.CompareTo(array[i]) == 0)
                {
                    index = i;
                }
            }

            return index;
        }
        private void ArrayRemove<R>(R[] array, int length, R key) where R : IComparable
        {
            var removeAt = ArrayIndex(array, length, key);

            array[removeAt] = default(R);

            for (int i = removeAt; i < length - 1; i++)
            {
                array[i] = array[i + 1];
            }
        }

        #endregion


        #region helper

        private int _nodeCount;

        public string CreateIdentifier()
        {
            return _nodeCount.ToString();
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
