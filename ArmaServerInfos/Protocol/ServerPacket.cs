
namespace ArmaServerInfo
{
    public class ServerPacket : Packet
    {
        public ServerPacket(byte packetType, byte[] buffer)
            : base(packetType, buffer)
        { }
        public override byte[] GetData()
        {
            if (this._buffer == null)
                return null;
            return this.GetSelectedBytes(this._buffer, 5, this._buffer.Length - 5);
        }
    }
}
