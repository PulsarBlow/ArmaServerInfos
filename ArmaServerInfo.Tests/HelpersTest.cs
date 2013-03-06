using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArmaServerInfo;

namespace ArmaServerInfo.Tests
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void ParseHexTest()
        {
            byte[] actual = Helpers.ParseHex("FFFF00FF00FF00FF");
            byte[] expected = { 255, 255, 00, 255, 00, 255, 00, 255 };
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
