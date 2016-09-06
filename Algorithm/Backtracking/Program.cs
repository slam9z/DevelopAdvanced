using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Algorithm
{

    public class RectanglePart
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
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

        public static int GreatestCommonDivisor(int[] source)
        {
            if (source.Length < 2)
            {
                throw new ArgumentException("Do not use this method if there are less than two numbers.");
            }

            var length = source.Length;

            int temp = GreatestCommonDivisor(source[length - 1], source[length - 2]);

            for (int i = length - 3; i >= 0; i--)
            {

                temp = GreatestCommonDivisor(temp, source[i]);
            }
            return temp;
        }

        public static int GreatestCommonDivisor(int a, int b)
        {
            int remainder;

            while (b != 0)
            {
                remainder = a % b;
                a = b;
                b = remainder;
            }

            return a;
        }


        #endregion

    }

    class Program
    {
        private static void CombineTest1()
        {
            var source1 = new List<RectanglePart>()
            {
                new RectanglePart("r1",8,3),
                new RectanglePart("r2",2,3),
                new RectanglePart("r3",6,3),
                new RectanglePart("r4",4,3),
            };
            var target = new RectanglePart("rt", 10, 6);

            var factory = new RectanglePartCombineFactory();
            factory.Combine(source1, target);


            //is correct result
            //Combined result
            //Name: r1 X:0 Y: 0 Width: 8 Height: 3
            //Name: r3 X:0 Y: 3 Width: 6 Height: 3
            //Name: r4 X:6 Y: 3 Width: 4 Height: 3
            //Name: r2 X:8 Y: 0 Width: 2 Height: 3

            var r1 = factory.SourceList.Where(h => h.Name == "r1").SingleOrDefault();
            var r2 = factory.SourceList.Where(h => h.Name == "r2").SingleOrDefault();
            var r3 = factory.SourceList.Where(h => h.Name == "r3").SingleOrDefault();
            var r4 = factory.SourceList.Where(h => h.Name == "r4").FirstOrDefault();


            if (
                   r1 != null && r1.X == 0 && r1.Y == 0
                && r2 != null && r2.X == 8 && r2.Y == 0
                && r3 != null && r3.X == 0 && r3.Y == 3
                && r4 != null && r4.X == 6 && r4.Y == 3
                )
            {
                Console.WriteLine("correct result");
            }
            else
            {
                Console.WriteLine("error result");
            }

        }

        private static void CombineTest2()
        {
            var source1 = new List<RectanglePart>()
            {
                new RectanglePart("r1",8,3),
                new RectanglePart("r2",2,3),
                new RectanglePart("r3",6,3),
            };
            var target = new RectanglePart("rt", 10, 6);

            var factory = new RectanglePartCombineFactory();
            factory.Combine(source1, target);

            //is current result
            //Combined result
            //Name: r1 X:0 Y: 0 Width: 8 Height: 3
            //Name: r3 X:0 Y: 3 Width: 6 Height: 3
            //Name: r4 X:6 Y: 3 Width: 4 Height: 3
            //Name: r2 X:8 Y: 0 Width: 2 Height: 3

        }


        static void Main(string[] args)
        {
            CombineTest1();
            //  CombineTest2();
            Console.ReadKey();
        }
    }

}