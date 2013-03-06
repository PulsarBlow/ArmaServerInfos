using ArmaServerInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ArmaServerInfo.Tests
{
    [TestClass]
    public class PacketCollectionTest
    {
        [TestMethod]
        public void GetDataTest()
        {
            List<byte[]> rawPackets = DataGenerator.GetInfoPackets();
            InfoPacket packet1 = new InfoPacket(rawPackets[0]);
            InfoPacket packet2 = new InfoPacket(rawPackets[1]);
            PacketCollection packets = new PacketCollection();
            packets.Add(packet1);
            packets.Add(packet2);


            byte[] result = packets.GetData();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.IsTrue(result.Length == packet1.GetData().Length + packet2.GetData().Length);
        }
        [TestMethod]
        public void GetDataStringTest()
        {
            List<byte[]> rawPackets = DataGenerator.GetInfoPackets();
            InfoPacket packet1 = new InfoPacket(rawPackets[0]);
            InfoPacket packet2 = new InfoPacket(rawPackets[1]);
            PacketCollection packets = new PacketCollection();
            packets.Add(packet1);
            packets.Add(packet2);

            string dataString1 = packet1.GetDataString();
            string dataString2 = packet2.GetDataString();
            string result = packets.GetDataString();
            Assert.IsTrue(!String.IsNullOrWhiteSpace(result));
            Assert.AreEqual<string>(packet1.GetDataString() + packet2.GetDataString(), result);
        }
    }
}