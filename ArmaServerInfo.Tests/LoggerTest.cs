using ArmaServerInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArmaServerInfo.Tests
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void GetFileNameHeaderTest()
        {
            string previous = null;
            for (int i = 0; i < 10; i++)
            {
                string current = Logger.GetFileNameHeader();
                Assert.IsTrue(current != previous);
            }
        }
    }
}
