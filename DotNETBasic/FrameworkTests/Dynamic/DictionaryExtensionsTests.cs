using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Dynamic.Tests
{
    [TestClass()]
    public class DictionaryExtensionsTests
    {

        private IDictionary<string, object> Card1 = new Dictionary<string, object>
        {

            ["Name"] = "John Smith",

            ["Age"] = 21,

            ["Address"] = new Dictionary<string, object>

            {

                ["Address1"] = "End of the world",

                ["City"] = "Nowheresvile"

            },

            ["Email"] = new Dictionary<string, object>
            {

                ["Value"] = "test@test.com",

            }

        };


        [TestMethod()]
        public void ToExpandoTest()
        {
            dynamic card1 = Card1.ToExpando();
            Assert.AreEqual(card1.Name, "John Smith");
            Assert.AreEqual(card1.Address.City, "Nowheresvile");


        }

    }
}