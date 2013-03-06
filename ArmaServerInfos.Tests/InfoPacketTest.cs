using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArmaServerInfo;
using System.Text;

namespace ArmaServerInfo.Tests
{
    [TestClass]
    public class InfoPacketTest
    {
        private const int DataStartPosition = 16;

        [TestMethod]
        public void GetBytesTest()
        {
            byte[] expected = DataGenerator.GetInfoPackets()[0];
            InfoPacket packet = new InfoPacket(expected);
            CollectionAssert.AreEqual(expected, packet.GetBytes());
        }
        [TestMethod]
        public void GetDataTest()
        {
            byte[] expected = DataGenerator.ExtractDataBytes(
                DataGenerator.GetInfoPackets()[0], DataStartPosition);
            InfoPacket packet = new InfoPacket(DataGenerator.GetInfoPackets()[0]);
            CollectionAssert.AreEqual(expected, packet.GetData());
        }
        [TestMethod]
        public void GetDataStringTest()
        {
            string expected = Encoding.ASCII.GetString(
                DataGenerator.ExtractDataBytes(
                    DataGenerator.GetInfoPackets()[0], DataStartPosition));

            InfoPacket packet = new InfoPacket(DataGenerator.GetInfoPackets()[0]);
            Assert.AreEqual<string>(expected, packet.GetDataString());
        }
    }
}
