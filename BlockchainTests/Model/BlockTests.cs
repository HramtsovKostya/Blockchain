using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Model.Tests
{
    [TestClass()]
    public class BlockTests
    {
        [TestMethod()]
        public void SerializeTest()
        {
            var block = new Block();

            var expectedString = 
                "{\"Created\":\"\\/Date(1535731200000+0800)\\/\"," +
                "\"Data\":\"Hello, world!\"," +
                "\"Hash\":\"00ac80c722316e1448ee19700b6232c03e" +
                    "b2c9855905b5599195b999d1840fa0\"," +
                "\"PreviousHash\":\"111111\"," +
                "\"UserName\":\"Admin\"}";

            var resultString = block.Serialize();
            var resultBlock = Block.Deserialize(resultString);

            Assert.AreEqual(expectedString, resultString);
            Assert.AreEqual(block.Hash, resultBlock.Hash);
            Assert.AreEqual(block.Created, resultBlock.Created);
            Assert.AreEqual(block.Data, resultBlock.Data);
            Assert.AreEqual(block.PreviousHash, resultBlock.PreviousHash);
            Assert.AreEqual(block.UserName, resultBlock.UserName);
        }
    }
}