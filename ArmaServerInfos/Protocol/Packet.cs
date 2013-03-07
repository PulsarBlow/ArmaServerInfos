using System;
using System.Text;

namespace ArmaServerInfo
{
    /// <summary>
    /// Base class for UPD packets data encapsulation.
    /// </summary>
    public class Packet
    {
        protected byte[] _buffer;
        private byte _packetType;

        /// <summary>
        /// Creates a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="buffer"></param>
        public Packet(byte[] buffer) : this(0, buffer) { }
        /// <summary>
        /// Creates a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="packetType"></param>
        /// <param name="buffer"></param>
        public Packet(byte packetType, byte[] buffer)
        {
            this._packetType = packetType;
            this._buffer = buffer;
        }
        /// <summary>
        /// Creates a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            return this._buffer;
        }
        /// <summary>
        /// Gets the underlying data as an ASCII encoded string.
        /// </summary>
        /// <returns></returns>
        public string GetDataString()
        {
            byte[] data = this.GetData();
            if (data == null)
                return null;
            return Encoding.ASCII.GetString(this.GetData());
        }
        /// <summary>
        /// Gets the underlying data
        /// </summary>
        /// <returns></returns>
        public virtual byte[] GetData()
        {
            return this._buffer;
        }
        /// <summary>
        /// Gets a portion of the source's bytes
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceIndex"></param>
        /// <returns></returns>
        protected byte[] GetSelectedBytes(byte[] source, int sourceIndex = 0)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            return this.GetSelectedBytes(source, sourceIndex, source.Length);
        }
        /// <summary>
        /// Gets a portion of the source's bytes
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
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
