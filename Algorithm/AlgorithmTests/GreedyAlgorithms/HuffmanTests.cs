using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.GreedyAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.GreedyAlgorithms.Tests
{
    [TestClass()]
    public class HuffmanTests
    {
        [TestMethod()]
        public void ComputeTest()
        {
            var huffman = new Huffman();
            var charItems = new List<Frequencytem<char>>()
            {
                new Frequencytem<char>() {Key='a' ,Frequency=45},
                new Frequencytem<char>() {Key='b' ,Frequency=13},
                new Frequencytem<char>() {Key='c' ,Frequency=12},
                new Frequencytem<char>() {Key='d' ,Frequency=16},
                new Frequencytem<char>() {Key='e' ,Frequency=9},
                new Frequencytem<char>() {Key='f' ,Frequency=5},


            };

            var tree = huffman.Compute(charItems);
            tree.Preorder(tree.Root, (o) => Console.WriteLine(o));

        }


    }
}