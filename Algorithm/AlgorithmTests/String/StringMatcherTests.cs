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
        //private IList<Tuple<string, string>> _data1 = new List<Tuple<string, string>>()
        //{
        //    new Tuple<string, string>("1a3ababababcaab","ababababca"),
        //    new Tuple<string, string>("ababababca123","ababababca"),
        //    new Tuple<string, string>("123ababababca","ababababca"),
        //    new Tuple<string, string>("123ababababbca","ababababca"),
        //    new Tuple<string, string>("ababababca","ababababca"),
        //};


        private IList<Tuple<string, string>> _data1 = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>(
                @"1a3wqeqewqweqweqwe
                2342342342wewrwerewrwe
                werwerwerwerwerwerwer
                dfwewe232423423423423423
                1a3wqeqewqweqweqwe
                2342342342wewrwerewrwe
                werwerwerwerwerwerwer
                dfwewe232423423423423423
                ababababcaab"
                ,"ababababca"),
        };

        public StringMatcherTests()
        {
            var builder = new StringBuilder();

            var length = int.MaxValue/256;

            for (int i = 0; i < length; i++)
            {
                builder.Append(length % 256);
            }

            var input = builder.ToString();

            _data1 = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>(
                input
                ,"ababababca"),
            };
        }

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

        /// <summary>
        /// 这个最快，开挂的不解释。
        /// 怎么比这个更快。
        /// </summary>
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
        public void FiniteAutomatonMatcherTest()
        {

            var automaton = new FiniteAutomatonMatcher();

            foreach (var data in _data1)
            {
                var index = automaton.Match(data.Item1, data.Item2);
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

            var m = pattern.Length + 1;
            for (int i = 0; i < m; i++)
            {
                Console.WriteLine();

                Console.Write($"{i}  ");
                for (int j = 0; j < epsilon.Count; j++)
                {

                    Console.Write($"{table[i, j]} ");
                }

                if (i < m - 1)
                {
                    Console.Write($" {pattern[i]}  ");
                }

            }
        }
    }
}