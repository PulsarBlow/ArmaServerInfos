using ArmaServerInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace ArmaServerInfo.Tests
{
    [TestClass]
    public class ChallengePacketTest
    {
        private const int DataStartPosition = 5;

        [TestMethod]
        public void GetBytesTest()
        {
            byte[] expected = DataGenerator.GetChallengePacket();
            byte[] data = DataGenerator.GetChallengePacket();
            ChallengePacket packet = new ChallengePacket(data);
            CollectionAssert.AreEqual(expected, packet.GetBytes());
        }
        [TestMethod]
        public void GetDataTest()
        {
            byte[] expected = DataGenerator.ExtractDataBytes(
                DataGenerator.GetChallengePacket(), DataStartPosition);
            byte[] data = DataGenerator.GetChallengePacket();
            ChallengePacket packet = new ChallengePacket(data);
            CollectionAssert.AreEqual(expected, packet.GetData());
        }
        [TestMethod]
        public void GetDataStringTest()
        {
            byte[] data = DataGenerator.ExtractDataBytes(
                DataGenerator.GetChallengePacket(), DataStartPosition);
            string expected = Encoding.ASCII.GetString(data);

            ChallengePacket packet = new ChallengePacket(DataGenerator.GetChallengePacket());
            Assert.AreEqual<string>(expected, packet.GetDataString());
        }
        [TestMethod]
        public void GetChallengeTest()
        {
            byte[] raw = new byte[] { 09, 04, 05, 06, 07, 0x2D, 49, 50, 51, 52, 53, 0 }; // ... -12345
            ChallengePacket packet = new ChallengePacket(raw);
            CollectionAssert.AreEqual(new byte[] { 255, 255, 207, 199 }, packet.GetChallenge());
        }        
    }
}
