using System;
using System.Globalization;
using System.IO;

namespace ArmaServerInfo
{
    /// <summary>
    /// Provides logging methods
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Logs a raw packet
        /// </summary>
        /// <param name="packet"></param>
        public static void Log(Packet packet)
        {
            if (packet == null)
                throw new ArgumentNullException("packet");

            string fileName = String.Format(CultureInfo.InvariantCulture,
                "Packets\\{0}.bin", GetFileNameHeader());
            Log(fileName, packet.GetBytes());
        }
        /// <summary>
        /// Logs a collection of raw packets
        /// </summary>
        /// <param name="packets"></param>
        public static void Log(PacketCollection packets)
        {
            if (packets == null)
                throw new ArgumentNullException("packets");

            foreach (var item in packets)
            {
                Log(item);
            }
        }
        /// <summary>
        /// Logs the data part of a raw packet
        /// </summary>
        /// <param name="packet"></param>
        public static void LogData(Packet packet)
        {
            if (packet == null)
                throw new ArgumentNullException("packet");

            string fileName = String.Format(CultureInfo.InvariantCulture,
                "Datas\\{0}.bin", GetFileNameHeader());
            Log(fileName, packet.GetBytes());
        }
        public static void LogData(PacketCollection packets)
        {
            if (packets == null)
                throw new ArgumentNullException("packets");

            string fileName = String.Format(CultureInfo.InvariantCulture,
                "DataStreams\\{0}.bin", GetFileNameHeader());
            Log(fileName, packets.GetData());
        }
        public static void LogData(string content)
        {
            string fileName = String.Format(CultureInfo.InvariantCulture,
                "Contents\\{0}.txt", GetFileNameHeader());
            Log(fileName, content);
        }
        public static void Log(string fileName, byte[] buffer)
        {
            if (String.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(buffer);
                }
            }
        }
        public static void Log(string fileName, string content)
        {
            if (String.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            File.WriteAllText(fileName, content);
        }
        public static string GetFileNameHeader()
        {
            DateTime now = DateTime.Now;
            return now.ToString("yyyyMMddHHmmssfffffff");
        }
    }
}
