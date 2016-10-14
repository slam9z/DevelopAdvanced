using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Concurrent;


namespace Garden.Cache
{
    /// <summary>
    /// Least Recently Used Cache
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class LRUCache<TKey, TValue>
    {

        private int _cacheSize;
        private int _currentSize;
        private ConcurrentDictionary<TKey, LinkedListNode<TValue>> _nodes;
        private LinkedList<TValue> _linkedList;

        private object _lockObject = new object();

        public LRUCache(int i)
        {
            _currentSize = 0;
            _cacheSize = i;
            _nodes = new ConcurrentDictionary<TKey, LinkedListNode<TValue>>(3, _cacheSize);
            _linkedList = new LinkedList<TValue>();
        }



        /**
         * 获取缓存中对象,并把它放在最前面
         */
        public TValue Get(TKey key)
        {
            LinkedListNode<TValue> node;
            _nodes.TryGetValue(key, out node);


            if (node != null)
            {
                lock (_lockObject)
                {
                    _linkedList.Remove(node);
                    _linkedList.AddFirst(node);
                }

                return node.Value;
            }

            else
            {
                return default(TValue);
            }
        }


        /**
         * 添加 entry到hashtable, 并把entry 
         */
        public void Put(TKey key, TValue value)
        {
            //先查看hashtable是否存在该entry, 如果存在，则只更新其value
            LinkedListNode<TValue> oldnode;
            LinkedListNode<TValue> newnode;

            newnode = new LinkedListNode<TValue>(value);

            _nodes.TryGetValue(key, out oldnode);


            lock (_lockObject)
            {
                if (oldnode == null)
                {
                    //缓存容器是否已经超过大小.
                    if (_currentSize >= _cacheSize)
                    {
                        LinkedListNode<TValue> lastNode;

                        var item = _nodes.Where(kvp => kvp.Value == _linkedList.Last).FirstOrDefault();

                        if (_nodes.TryRemove(item.Key, out lastNode))
                        {
                            _linkedList.RemoveLast();
                        }

                    }
                    else
                    {
                        _currentSize++;
                    }

                    _linkedList.AddFirst(newnode);
                    _nodes.TryAdd(key, newnode);

                }

                else
                {
                    _linkedList.Remove(oldnode);
                    _linkedList.AddFirst(newnode);

                    if (Comparer<TValue>.Default.Compare(oldnode.Value, newnode.Value) != 0)
                    {
                        _nodes.TryUpdate(key, oldnode, newnode);
                    }
                }
            }
        }


        public void Remove(TKey key)
        {
            LinkedListNode<TValue> node;
            _nodes.TryGetValue(key, out node);

            if (node != null)
            {
                lock (_lockObject)
                {
                    _linkedList.Remove(node);
                    LinkedListNode<TValue> removeNode;
                    if (_nodes.TryRemove(key, out removeNode))
                    {

                    }
                    _currentSize--;
                }
            }

        }


        /*
         * 清空缓存
         */
        public void Clear()
        {
            lock (_lockObject)
            {
                _linkedList.Clear();
                _currentSize = 0;
                _nodes.Clear();
            }
        }

        public int Count
        {
            get
            {
                return _currentSize;
            }
        }




    }


}
