using System;
using System.Collections.Generic;
using System.IO;

namespace ArmaServerInfo
{
    public static class Helpers
    {
        private static int PacketCounter = 0;
        private static int QueryResponseCounter = 0;
        public static byte[] GetTimeStamp()
        {
#if DEBUG
            return new byte[] { 0x06, 0x06, 0x06, 0x06 }; // Fixed timestamp footprint for easier packet debugging
#else

            DateTime now = DateTime.Now;
            DateTime epoch = new DateTime(1970, 1, 1);
            return BitConverter.GetBytes((int)(now - epoch).TotalSeconds);
#endif

        }
        public static int GetTimeStampInt()
        {
            return BitConverter.ToInt32(GetTimeStamp(), 0);
        }

        /// <summary>
        /// Converts a hex string to its byte array equivalent
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/a/14335076</remarks>
        public static byte[] ParseHex(string hexString)
        {
            if ((hexString.Length & 1) != 0)
            {
                throw new ArgumentException("Input must have even number of characters");
            }
            byte[] ret = new byte[hexString.Length / 2];
            for (int i = 0; i < ret.Length; i++)
            {
                int high = ParseNybble(hexString[i * 2]);
                int low = ParseNybble(hexString[i * 2 + 1]);
                ret[i] = (byte)((high << 4) | low);
            }

            return ret;
        }

        private static int ParseNybble(char c)
        {
            unchecked
            {
                uint i = (uint)(c - '0');
                if (i < 10)
                    return (int)i;
                i = ((uint)c & ~0x20u) - 'A';
                if (i < 6)
                    return (int)i + 10;
                throw new ArgumentException("Invalid nybble: " + c);
            }
        }
        public static void DumpDataPackets(SortedDictionary<byte, byte[]> dataPackets)
        {
            if (dataPackets == null || dataPackets.Count == 0)
                return;
            List<byte> data = new List<byte>();
            foreach (var item in dataPackets)
            {
                if (item.Value == null || item.Value.Length == 0)
                    continue;
                data.AddRange(item.Value);
            }
            if (data.Count == 0)
                return;
            if (!Directory.Exists("QueryResponses"))
                Directory.CreateDirectory("QueryResponses");
            using (FileStream stream = new FileStream(String.Format("QueryResponses\\{0:000}-{1}.bin", QueryResponseCounter, Guid.NewGuid().ToString()), FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(data.ToArray());
                }
            }
            QueryResponseCounter++;
        }
        public static void DumpServerResponse(byte[] response)
        {
            if (!Directory.Exists("Packets"))
                Directory.CreateDirectory("Packets");
            using (FileStream stream = new FileStream(String.Format("Packets\\{0:0000}-{1}.bin", PacketCounter, Guid.NewGuid().ToString()), FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(response);
                }
            }
            PacketCounter++;
        }
        public static byte[] GetPackedBytes(int value)
        {
            return new byte[] {
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)(value >> 0)
            };
        }
    }
}
