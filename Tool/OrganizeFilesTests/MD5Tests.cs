using Microsoft.VisualStudio.TestTools.UnitTesting;
using HashEncrypted;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashEncrypted.Tests
{
    [TestClass()]
    public class MD5Tests
    {
        [TestMethod()]
        public void MD5Test()
        {
            Assert.Fail();
        }

        public static void PrintBytes(byte[] input)
        {
            Console.WriteLine();
            foreach (var i in input)
            {
                Console.Write("{0:x}\t", i);
            }
        }


        private static void StringTest(string input)
        {

            MD5 m = new MD5(input);
            Console.WriteLine("MD5(\"" + input + "\")=" + m.MD5HexOutput);
        }

        private static void Test()
        {
            //MD5 k = new MD5(new byte[]{});
            //Console.WriteLine("MD5(\""  + "\")=" + k.MD5HexOutput);
            MD5 a = new MD5(new byte[] { 97 });
            Console.WriteLine("MD5(\"a\")=" + a.MD5HexOutput);
            MD5 abc = new MD5(new byte[] { 97, 98, 99 });
            Console.WriteLine("MD5(\"abc\")=" + abc.MD5HexOutput);
        }

        static void Main(string[] args)
        {
            //  MD5 ("") = d41d8cd98f00b204e9800998ecf8427e
            //  MD5 ("a") = 0cc175b9c0f1b6a831c399e269772661
            //  MD5 ("abc") = 900150983cd24fb0d6963f7d28e17f72
            //  MD5 ("message digest") = f96b697d7cb7938d525a2f31aaf161d0
            //  MD5 ("abcdefghijklmnopqrstuvwxyz") = c3fcd3d76192e4007dfb496cca67e13b
            //  MD5 ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789") =
            //d174ab98d277d9f5a5611c2c9f419d9f
            //  MD5 ("123456789012345678901234567890123456789012345678901234567890123456789
            //01234567890") = 57edf4a22be3c955ac49da2e2107b67a
            // Test();

            // Console.WriteLine(MyMD5.Test("a")); 

            // Console.WriteLine();
            StringTest("");
            StringTest("a");
            StringTest("abc");
            StringTest("message digest");
        }
    }
}