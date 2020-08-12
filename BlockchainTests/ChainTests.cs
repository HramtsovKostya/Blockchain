using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blockchain.Model;

namespace Blockchain.Tests
{
    [TestClass()]
    public class ChainTests
    {
        [TestMethod()]
        public void ChainTest()
        {
            var chain = new Chain();
            chain.Add("Code blog", "Admin");
         
            Assert.AreEqual("Code blog", chain.Last.Data);
        }

        [TestMethod()]
        public void CheckTest()
        {
            var chain = new Chain();
            chain.Add("Hello, world!", "Admin");
            chain.Add("Code blog", "Shwanov");

            Assert.IsTrue(chain.Check());
        }
    }
}