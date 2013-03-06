
namespace ArmaServerInfo
{
    public class InfoPacket : ServerPacket
    {        
        public InfoPacket(byte[] buffer)
            : base((byte)PacketTypes.ServerInfo, buffer)
        { }
        public override byte[] GetData()
        {
            if (this._buffer == null)
                return null;
            return this.GetSelectedBytes(this._buffer, 16, this._buffer.Length - 16);
        }
    }
}
