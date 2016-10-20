using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FrameworkTests.Encode
{
    [TestClass()]
    public class EncodeTests
    {
        [TestMethod()]
        public void UrlEncodeTest()
        {
            string url = "C++ C#";
            UrlEncode(url);

            url = "业务主题管理";
            UrlEncode(url);
        }

        private void UrlEncode(string url)
        {
            Console.WriteLine(HttpUtility.UrlEncode(url));//C%2b%2b+C%23
            Console.WriteLine(HttpUtility.UrlPathEncode(url));//C++%20C#
            Console.WriteLine(Uri.EscapeUriString(url));//C++%20C#
            Console.WriteLine(Uri.EscapeDataString(url));//C%2B%2B%20C%23
        }

        [TestMethod()]
        public void UrlEscapeDataStringTest()
        {
            string url = "C++ C#";
            UrlEscapeDataString(url);

            url = "业务主题管理";
            UrlEscapeDataString(url);
        }
        private void UrlEscapeDataString(string url)
        {
            var escapeUrl = Uri.EscapeDataString(url);
            Console.WriteLine(escapeUrl);
            var unEscapeUrl = Uri.UnescapeDataString(escapeUrl);
            Console.WriteLine(unEscapeUrl);

            Console.WriteLine(Uri.EscapeDataString(escapeUrl));
        }



        [TestMethod()]
        public void UrlEscapeUriStringTest()
        {
            string url = "C++ C#";
            UrlEscapeUriString(url);

            url = "业务主题管理";
            UrlEscapeUriString(url);
        }
        private void UrlEscapeUriString(string url)
        {
            var escapeUrl = Uri.EscapeUriString(url);
            Console.WriteLine(escapeUrl);
            var unEscapeUrl = Uri.UnescapeDataString(escapeUrl);
            Console.WriteLine(unEscapeUrl);

            Console.WriteLine(Uri.EscapeUriString(escapeUrl));
        }
    }
}
