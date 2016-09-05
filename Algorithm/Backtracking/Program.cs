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
        public bool IsBottomDocked { get; set; }
        public bool IsRightDocked { get; set; }
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

        public void Combine(IList<RectanglePart> sourceList, RectanglePart target)
        {
            //先按比例从大到小的顺序优化搜索。
            //_sourceList = sourceList.OrderByDescending
            //    (i =>
            //        Math.Max((double)i.Width / target.Width, (double)(i.Height) / target.Height)
            //    ).ToList();

            _sourceList = sourceList.OrderByDescending
                (i =>
                    Math.Max(i.Width / target.Width, (i.Height) / target.Height)
                ).ToList();

            _handlderList = new List<RectanglePart>();

            _target = target;
            _hasResult = false;
            CombineCore(0);
        }


        public virtual void OutputCombinedRectanglePart()
        {
            Console.WriteLine("Combined result");
            foreach (var handler in _handlderList)
            {
                Console.WriteLine(handler);
            }
        }

        /// <summary>
        /// 使用回溯算法递归解决!没写好，感觉和枚举没啥区别。
        /// </summary>
        /// <param name="arrangeIndex"></param>
        private void CombineCore(int arrangedCount)
        {
            //只求唯一解
            if (_hasResult)
            {
                return;
            }

            //结束条件。
            if (arrangedCount >= _sourceList.Count)
            {
                OutputCombinedRectanglePart();
                _hasResult = true;
                return;
            }

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
                        CombineCore(arrangedCount + 1);
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
                            CombineCore(arrangedCount + 1);
                            Clear();
                        }
                    }
                }

            }
        }

        private void Clear()
        {
            if (_handlderList.Count >= 1)
            {
                var removeItem = _handlderList.Last();
                removeItem.X = 0;
                removeItem.Y = 0;
                removeItem.IsBottomDocked = false;
                removeItem.IsRightDocked = false;
                removeItem.IsHandle = false;

                _handlderList.Remove(removeItem);
            }

        }


        /// <summary>
        /// 检查一个矩形是否可以放置在另一个举行下侧或者右侧。
        /// </summary>
        /// <param name="dockArranged"></param>
        /// <returns></returns>
        private bool CheckAndCombine(RectanglePart dockArranged, RectanglePart arrange)
        {
            //为什么会出现重复?清理还没处理
            if (dockArranged == arrange)
            {
                return false;
            }

            //没有被处理的元素。
            if (dockArranged == null)
            {
                arrange.X = 0;
                arrange.Y = 0;
                arrange.IsHandle = true;

                _handlderList.Add(arrange);

                Console.WriteLine("step: {0}", arrange);
                return true;
            }

            //检查下界
            if (!dockArranged.IsBottomDocked &&
                dockArranged.Y + dockArranged.Height + arrange.Height <= _target.Height
                )
            {
                dockArranged.IsBottomDocked = true;

                arrange.X = dockArranged.X;
                arrange.Y = dockArranged.Height + dockArranged.Y;
                arrange.IsHandle = true;

                _handlderList.Add(arrange);

                Console.WriteLine("step: {0}", arrange);
                return true;
            }


            //检查右界

            if (!dockArranged.IsRightDocked &&
                dockArranged.X + dockArranged.Width + arrange.Width <= _target.Width
                )
            {
                dockArranged.IsRightDocked = true;

                arrange.X = dockArranged.Width + dockArranged.X;
                arrange.Y = dockArranged.Y;
                arrange.IsHandle = true;

                _handlderList.Add(arrange);


                Console.WriteLine("step: {0}", arrange);
                return true;

            }

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


            //is current result
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