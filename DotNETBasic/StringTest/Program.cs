using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringTest
{

    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return String.IsNullOrWhiteSpace(s);
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            string key = null;
            key.IsNullOrWhiteSpace();
        }
    }
}
