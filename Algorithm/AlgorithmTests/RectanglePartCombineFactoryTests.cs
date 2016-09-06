using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Tests
{
    [TestClass()]
    public class RectanglePartCombineFactoryTests
    {

        public void CombineTest1()
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
                Assert.Fail("error result");
            }

        }

        private void CombineTest2()
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

        }

        [TestMethod()]
        public void CombineTest()
        {
            CombineTest1();
            CombineTest2();
        }
    }
}