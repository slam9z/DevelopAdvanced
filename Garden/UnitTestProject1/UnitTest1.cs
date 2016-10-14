using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Garden.Cache;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LRUCache<String, String> c = new LRUCache<String, String>(3);
            c.Put("1", "one");                           // 1  
            c.Put("2", "two");                           // 2 1  
            c.Put("3", "three");                         // 3 2 1  
            c.Put("4", "four");                          // 4 3 2  
            c.Get("2");                                  // 2 4 3 
            c.Remove("1");
            c.Put("5", "five");                          // 5 2 4  
            c.Put("4", "second four");                   // 4 5 2  
            c.Get("2");
            c.Get("3");
        }



        [TestMethod]
        public void TestMethodthread()
        {
          
            var c = new LRUCache<int, String>(1000);
          

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 500; i++)
                {
                    c.Put(i, i.ToString());
                }

                Console.WriteLine(" Put1 [{0}] ", c.Count);
            }

                );

            Task.Factory.StartNew(() =>
             {
                 for (int i = 500; i < 1000; i++)
                 {
                     c.Put(i, i.ToString());
                 }
                 Console.WriteLine("Put 2 [{0}] ", c.Count);
             }

            );


            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    var v = c.Get(i);


                
                }

                Console.WriteLine("get [{0}] ", c.Count);
            });


        }


    }
}
