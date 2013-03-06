using System;
using System.Text;

namespace ArmaServerInfo
{
    public class Packet
    {
        protected byte[] _buffer;
        private byte _packetType;

        public Packet(byte[] buffer) : this(0, buffer) { }
        public Packet(byte packetType, byte[] buffer)
        {
            this._packetType = packetType;
            this._buffer = buffer;
        }

        public byte[] GetBytes()
        {
            return this._buffer;
        }
        public string GetDataString()
        {
            byte[] data = this.GetData();
            if (data == null)
                return null;
            return Encoding.ASCII.GetString(this.GetData());
        }
        public virtual byte[] GetData()
        {
            return this._buffer;
        }
        protected byte[] GetSelectedBytes(byte[] source, int sourceIndex = 0)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            return this.GetSelectedBytes(source, sourceIndex, source.Length);
        }
        protected byte[] GetSelectedBytes(byte[] source, int sourceIndex, int length)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            byte[] result = new byte[length];
            Array.Copy(source, sourceIndex, result, 0, length);
            return result;
        }
    }
}
