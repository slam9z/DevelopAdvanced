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
    public class MapBinanyHeap<T> :  BinanyHeap<T> where T : IMapIndex
    {
        public MapBinanyHeap(IList<T> source, Func<T, T, bool> com) : base(source, com)
        {
        }

        public override T Extract()
        {
            _source[_heapSize-1].MapIndex = 0;
            return base.Extract();

        }

        public override void Insert(T key)
        {
            key.MapIndex = _heapSize;
            base.Insert(key);
        }

        /// <summary>
        /// 就为了这个UpdateKey真是煞费苦心
        /// </summary>
        /// <param name="oldKey"></param>
        /// <param name="newKey"></param>
        public void UpdateKey(T node, T newNode)
        {
            UpdateKey(node.MapIndex + 1, newNode);
        }

        protected override void Build(IList<T> source, Func<T, T, bool> com)
        {
            var index = -1;
            foreach (var item in source)
            {
                index++;
                item.MapIndex = index;
            }

            base.Build(source, com);
        }

        protected override void Exchange(IList<T> source, int indexa, int indexb)
        {
            base.Exchange(source, indexa, indexb);
            source[indexb].MapIndex = indexb;
            source[indexa].MapIndex = indexa;
        }


   
    }
}
