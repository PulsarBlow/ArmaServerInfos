using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ArmaServerInfo
{
    class QueryClient
    {
        private readonly byte[] timestamp = Helpers.GetTimeStamp();

        public PacketCollection QueryServer(NetworkSettings nwSettings)
        {
            if (nwSettings == null)
                throw new ArgumentNullException("nwSettings");

            PacketCollection packets = new PacketCollection();

            using (UdpClient client = new UdpClient(nwSettings.LocalPort))
            {
                byte[] request, response,
                    timestamp = Helpers.GetTimeStamp();

                IPEndPoint remoteIpEndpoint = new IPEndPoint(IPAddress.Parse(nwSettings.Host), nwSettings.RemotePort);
                client.Client.ReceiveTimeout = nwSettings.ReceiveTimeout;
                client.Connect(remoteIpEndpoint);
                
                request = GetChallengeRequest(timestamp);
                client.Send(request, request.Length);
                response = client.Receive(ref remoteIpEndpoint);
                ChallengePacket pChallenge = new ChallengePacket(response);

                request = GetInfosRequest(timestamp, pChallenge.GetChallenge());
                client.Send(request, request.Length);
                
                while (true)
                {
                    try
                    {
                        response = client.Receive(ref remoteIpEndpoint);
                        packets.Add(new InfoPacket(response));
                    }
                    catch (SocketException ex)
                    {
                        if (ex.ErrorCode == (int)SocketError.TimedOut)
                            break;
                        else
                            throw;
                    }
                }
            }

            return packets;
        }

        private byte[] GetChallengeRequest(byte[] timestamp)
        {
            return GetRequestHeader((byte)PacketTypes.Challenge, timestamp);
        }
        private byte[] GetInfosRequest(byte[] timestamp, byte[] challenge)
        {                        
            List<byte> result = new List<byte>();
            result.AddRange(GetRequestHeader((byte)PacketTypes.ServerInfo, timestamp));
            result.AddRange(challenge);
            result.AddRange(new byte[] { 0xff, 0xff, 0xff, 0x01 });
            return result.ToArray();
        }
        private byte[] GetRequestHeader(byte requestType, byte[] timestamp)
        {
            List<byte> result = new List<byte>();
            result.AddRange(new byte[] { 0xfe, 0xfd });
            result.Add(requestType);
            result.AddRange(timestamp);
            return result.ToArray();
        }
    }
}
