
namespace ArmaServerInfo
{
    public class NetworkSettings
    {
        public string Host { get; set; }
        public int LocalPort { get; set; }
        public int RemotePort { get; set; }
        public int ReceiveTimeout { get; set; }

        public static NetworkSettings GetDefault(string host = "127.0.0.1", int port = 2302)
        {
            return new NetworkSettings
            {
                Host = host,
                LocalPort = 56800,
                RemotePort = port,
                ReceiveTimeout = 100
            };
        }
    }
}
