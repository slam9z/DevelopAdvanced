using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class RectanglePart
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        //考虑实际数学坐标，需要换成TopNode。感觉计算也不需要转变啊
        public RectanglePart BottomNode { get; set; }
        public RectanglePart RightNode { get; set; }
        public bool IsHandle { get; set; }

        public RectanglePart()
        {

        }

        public RectanglePart(string name, int width, int height)
        {
            Name = name;
            Height = height;
            Width = width;
        }

        public override string ToString()
        {
            return string.Format(
                "Name:{0} X:{1} Y:{2} Width:{3} Height: {4}"
                , Name
                , X
                , Y
                , Width
                , Height
                );

            //   return string.Format($"Name:{Name} X:{X} Y:{Y} Width:{Width} Height: {Height}");
        }

    }

    public class RectanglePartCombineFactory
    {
        private RectanglePart _target;

        private IList<RectanglePart> _sourceList;

        public IList<RectanglePart> SourceList
        {
            get
            {
                return _sourceList;
            }
        }


        private IList<RectanglePart> _handlderList;


        private bool _hasResult;

        public Action<IList<RectanglePart>> OutputCombinedResult { get; set; }

        public RectanglePartCombineFactory()
        {

        }


        /// <summary>
        /// not thread-safe
        /// </summary>
        /// <param name="sourceList"></param>
        /// <param name="target"></param>
        public void Combine(IList<RectanglePart> sourceList, RectanglePart target)
        {
            //先按比例从大到小的顺序优化搜索。
            _sourceList = sourceList.OrderByDescending
                (i =>
                    Math.Max((double)i.Width / target.Width, (double)(i.Height) / target.Height)
                ).ToList();

            _handlderList = new List<RectanglePart>();

            _target = target;
            _hasResult = false;
            CombineCore();
        }


        public virtual void OutputCombinedRectanglePart()
        {
            OutputCombinedResult?.Invoke(_handlderList);

            Console.WriteLine("Combined result");
            foreach (var handler in _handlderList)
            {
                Console.WriteLine(handler);
            }
        }

        private void PrintHandlderList()
        {
            //Console.WriteLine("PrintHandlderList");
            //foreach (var handler in _handlderList)
            //{
            //    Console.Write("{0}, ", handler.Name);
            //}
            //Console.WriteLine("");
        }

        /// <summary>
        /// 使用回溯算法递归。
        /// </summary>
        private void CombineCore()
        {
            //只求唯一解
            if (_hasResult)
            {
                return;
            }

            //结束条件。
            if (_handlderList.Count == _sourceList.Count)
            {
                OutputCombinedRectanglePart();

                PrintHandlderList();
                _hasResult = true;
                return;
            }

            //利用循环递归回溯
            for (var i = 0; i <= _sourceList.Count - 1; i++)
            {

                var currentArrange = _sourceList[i];
                if (currentArrange.IsHandle)
                {
                    continue;
                }

                //给现有节点添加子节点
                if (_handlderList.Count == 0)
                {
                    if (CheckAndCombine(null, currentArrange))
                    {
                        CombineCore();
                        if (_hasResult)
                        {
                            return;
                        }
                        Clear();
                    }
                }
                else
                {
                    for (var j = 0; j <= _handlderList.Count - 1; j++)
                    {
                        var dockItem = _handlderList[j];

                        if (CheckAndCombine(dockItem, currentArrange))
                        {
                            CombineCore();
                            if (_hasResult)
                            {
                                return;
                            }
                            Clear();
                        }
                    }
                }
            }
        }

        private void Clear()
        {

            var removeNode = _handlderList.LastOrDefault();
            if (removeNode != null)
            {
                removeNode.X = 0;
                removeNode.Y = 0;
                removeNode.BottomNode = null;
                removeNode.RightNode = null;
                removeNode.IsHandle = false;
                _handlderList.Remove(removeNode);

                //清除父节点的引用
                var parentNode = _handlderList.LastOrDefault();
                if (parentNode != null)
                {
                    if (parentNode.BottomNode == removeNode)
                    {
                        parentNode.BottomNode = null;
                    }
                    else if (parentNode.RightNode == removeNode)
                    {
                        parentNode.RightNode = null;
                    }

                }
                PrintHandlderList();
            }
        }


        /// <summary>
        /// 检查一个矩形是否可以放置在另一个举行下侧或者右侧，这是一个构建二叉树的过程
        /// </summary>
        /// <param name="dockArranged"></param>
        /// <returns></returns>
        private bool CheckAndCombine(RectanglePart dockArranged, RectanglePart arrange)
        {
            //处理重复出现的节点，只要调用方正确，不应该出现这种情况。
            if (dockArranged == arrange)
            {
                return false;
            }

            //处理根节点
            if (dockArranged == null)
            {
                arrange.X = 0;
                arrange.Y = 0;
                arrange.IsHandle = true;

                _handlderList.Add(arrange);

                PrintHandlderList();
                return true;
            }

            //添加下面的节点
            if (dockArranged.BottomNode == null
                && dockArranged.X + arrange.Width <= _target.Width
                && dockArranged.Y + dockArranged.Height + arrange.Height <= _target.Height

                )
            {
                dockArranged.BottomNode = arrange;

                arrange.X = dockArranged.X;
                arrange.Y = dockArranged.Height + dockArranged.Y;
                arrange.IsHandle = true;

                _handlderList.Add(arrange);

                PrintHandlderList();

                return true;
            }


            //添加右边节点

            if (dockArranged.RightNode == null
                && dockArranged.X + dockArranged.Width + arrange.Width <= _target.Width
                && dockArranged.Y + arrange.Height <= _target.Height
                )
            {
                dockArranged.RightNode = arrange;

                arrange.X = dockArranged.Width + dockArranged.X;
                arrange.Y = dockArranged.Y;
                arrange.IsHandle = true;

                _handlderList.Add(arrange);

                PrintHandlderList();

                return true;

            }
            PrintHandlderList();
            return false;
        }


        #region unit  可能简化计算难度

        public int WidthUnit { get; set; }
        public int HeightUnit { get; set; }

        public void CalcUnit(IEnumerable<RectanglePart> list)
        {
        }


        #endregion

    }

}
