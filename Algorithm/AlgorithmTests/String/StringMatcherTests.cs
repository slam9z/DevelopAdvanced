using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithm.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmTests;

namespace Algorithm.String.Tests
{
    [TestClass()]
    public class StringMatcherTests
    {
        private IList<Tuple<string, string>> _data1 = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("1a3ababababcaab","ababababca"),
            new Tuple<string, string>("ababababca123","ababababca"),
            new Tuple<string, string>("123ababababca","ababababca"),
            new Tuple<string, string>("123ababababbca","ababababca"),
            new Tuple<string, string>("ababababca","ababababca"),
        };

        [TestMethod()]
        public void KmpMatchTest()
        {

            var kmp = new KmpMatcher();

            foreach (var data in _data1)
            {
                var index = kmp.Match(data.Item1, data.Item2);
                Assert.AreEqual(data.Item1.IndexOf(data.Item2), index);
            }
        }


        [TestMethod()]
        public void NaiveStringMathcerTest()
        {

            var kmp = new NaiveStringMathcer();

            foreach (var data in _data1)
            {
                var index = kmp.Match(data.Item1, data.Item2);
                Assert.AreEqual(data.Item1.IndexOf(data.Item2), index);
            }
        }


        [TestMethod()]
        public void StringIndexMathcerTest()
        {

            var kmp = new NaiveStringMathcer();

            foreach (var data in _data1)
            {
                var index = data.Item1.IndexOf(data.Item2);
                Assert.AreEqual(data.Item1.IndexOf(data.Item2), index);
            }
        }


        [TestMethod()]
        public void ComputeTransitionFunctionTest()
        {

            var automaton = new FiniteAutomatonMatcher();

            var epsilon = new List<char> { 'a', 'b', 'c' };
            var pattern = "ababaca";

            var table = automaton.ComputeTransitionFunction(pattern, epsilon);


            Console.Write("    ");
            foreach (var item in epsilon)
            {

                Console.Write($"{item} ");
            }

            var m = pattern.Length;
            for (int i = 0; i < m; i++)
            {
                Console.WriteLine();

                Console.Write($"{i}  ");
                for (int j = 0; j < epsilon.Count; j++)
                {

                    Console.Write($"{table[i, j]} ");
                }


                Console.Write($" {pattern[i]}  ");


            }
        }
    }
}