﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.DynamicProgramming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.DynamicProgramming.Tests
{
    [TestClass()]
    public class OptimalBinarySearchTreeTests
    {
        [TestMethod()]
        public void ComputeTest()
        {
            var keys = new string[]
            {
                "k1",
                "k2",
                "k3",
                "k4",
                "k5",
            };

            var ds = new string[]
            {
                "d0",
                "d1",
                "d2",
                "d3",
                "d4",
                "d5",
            };

            //添加一个虚拟的节点
            var k = new int[]
                 {
                     0,
                     15,10
                    ,5,10
                    ,20
                 };
            var d = new int[]
            {
                5,10
                ,5, 5
                ,5,10
            };

            Assert.AreEqual(k.Sum() + d.Sum(), 100);

            var obst = new OptimalBinarySearchTree<string>();
            var e = obst.Compute(k, d, 5);

            Assert.AreEqual(e, 275);

            obst.PrintRoot();
            obst.Create(keys, ds);

            Console.WriteLine();
            obst.Inorder(obst.Root, (node) => { Console.Write($"{node} "); });

        }
    }
}