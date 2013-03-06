using ArmaServerInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ArmaServerInfo.Tests
{
    [TestClass]
    public class PacketTest
    {
        [TestMethod]
        public void GetDataTest()
        {
            byte[] buffer = DataGenerator.GetChallengePacket();
            Packet packet1 = new Packet(buffer);
            CollectionAssert.AreEqual(buffer, packet1.GetBytes());
            string dataString1 = packet1.GetDataString();
            Assert.IsFalse(String.IsNullOrWhiteSpace(dataString1));

            List<byte[]> bufferCollection = DataGenerator.GetInfoPackets();

            Packet packet2 = new Packet(bufferCollection[0]);
            CollectionAssert.AreEqual(bufferCollection[0], packet2.GetBytes());
            string dataString2 = packet2.GetDataString();
            Assert.IsFalse(String.IsNullOrWhiteSpace(dataString2));

            Packet packet3 = new Packet(bufferCollection[1]);
            CollectionAssert.AreEqual(bufferCollection[1], packet3.GetBytes());
            string dataString3 = packet3.GetDataString();
            Assert.IsFalse(String.IsNullOrWhiteSpace(dataString3));

        }
    }
}
