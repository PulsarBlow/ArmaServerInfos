using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaServerInfo
{
    public class PacketCollection : List<Packet>
    {
        public PacketCollection()
            : base()
        { }
        public PacketCollection(int capacity)
            : base(capacity)
        { }
        public PacketCollection(IEnumerable<Packet> collection)
            : base(collection)
        { }        
        public byte[] GetData()
        {
            List<byte> result = new List<byte>();
            foreach (var packet in this)
            {
                result.AddRange(packet.GetData());
            }
            return result.ToArray();
        }
        public string GetDataString()
        {
            return CleanDataString(Encoding.ASCII.GetString(this.GetData()));
        }
        protected string CleanDataString(string rawDataString)
        {
            if (String.IsNullOrWhiteSpace(rawDataString))
                return rawDataString;
            string rs = Encoding.ASCII.GetString(new byte[] { 0x70, 0x6C, 0x61, 0x79, 0x65, 0x72, 0x5F, 0x00, 0x1E });
            string result = rawDataString.Replace(rs, "");
            return result;
        }
    }
}
