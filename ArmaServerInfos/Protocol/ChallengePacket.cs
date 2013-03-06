
using System;
namespace ArmaServerInfo
{
    public class ChallengePacket : ServerPacket
    {
        public ChallengePacket(byte[] buffer)
            : base((byte)PacketTypes.Challenge, buffer)
        { }
        public byte[] GetChallenge()
        {
            int challenge = Int32.Parse(GetDataString());
            return new byte[] {
                (byte)(challenge >> 24),
                (byte)(challenge >> 16),
                (byte)(challenge >> 8),
                (byte)(challenge >> 0)
            };
        }
    }
}
