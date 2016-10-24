using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Struct
{
    /// <summary>
    /// 为了解决更新key,必须要知道index这个鸡肋，鬼才知道index。
    /// 二叉堆这点有点恶心
    /// 
    /// 如果没有源码那不要重写吗?
    /// </summary>
    public class ExtentionBinanyHeap<T> : BinanyHeap<T>
    {
        public ExtentionBinanyHeap(IList<T> source, Func<T, T, bool> com) : base(source, com)
        {
        }

        public override T Extract()
        {
            SetMapIndex(_source[_heapSize - 1], 0);
            SetBelonged(_source[0], null);
            return base.Extract();

        }

        public override void Insert(T key)
        {
            SetMapIndex(key, _heapSize);
            SetBelonged(key, this);
            base.Insert(key);
        }

        /// <summary>
        /// 就为了这个UpdateKey真是煞费苦心
        /// </summary>
        /// <param name="oldKey"></param>
        /// <param name="newKey"></param>
        public void UpdateKey(T node, T newNode)
        {
            var mapNode = node as IMapIndex;
            if (mapNode != null)
            {
                UpdateKey(mapNode.MapIndex + 1, newNode);
            }
            else
            {
                throw new InvalidOperationException("node must is IMapIndex");
            }
        }

        protected override void Build(IList<T> source, Func<T, T, bool> com)
        {
            var index = -1;
            foreach (var item in source)
            {
                index++;
                SetMapIndex(item, index);
                SetBelonged(item,this);
            }

            base.Build(source, com);
        }

        protected override void Exchange(IList<T> source, int indexa, int indexb)
        {
            base.Exchange(source, indexa, indexb);

            SetMapIndex(source[indexb], indexb);
            SetMapIndex(source[indexa], indexa);
        }

        private bool? _hasMapIndex;

        /// <summary>
        /// handler map index
        /// </summary>
        /// <param name="node"></param>
        /// <param name="index"></param>
        private void SetMapIndex(T node, int index)
        {
            if (_hasMapIndex.HasValue&&!_hasMapIndex.Value)
            {
                return;
            }

            if (!_hasMapIndex.HasValue)
            {
                _hasMapIndex = node is IMapIndex;
                if (!_hasMapIndex.Value)
                {
                    return;
                }
            }
            (node as IMapIndex).MapIndex = index;
        }

        private bool? _hasBelonged;


        /// <summary>
        /// is belongedTo
        /// </summary>
        /// <param name="node"></param>
        /// <param name="belongedTo"></param>
        private void SetBelonged(T node, object belongedTo)
        {
            if (_hasBelonged.HasValue&&!_hasBelonged.Value)
            {
                return;
            }

            if (!_hasBelonged.HasValue)
            {
                _hasBelonged = node is IBelonged;
                if (!_hasBelonged.Value)
                {
                    return;
                }
            }
            (node as IBelonged).BelongedTo = belongedTo;
        }

    }
}
