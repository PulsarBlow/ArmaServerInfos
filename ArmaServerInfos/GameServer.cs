using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace ArmaServerInfo
{
    /// <summary>
    /// Encapsultates the game server data
    /// </summary>
    public class GameServer
    {
        #region Members & Properties
        private const string DefaultName = "Unknown server";
        public NetworkSettings Settings { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool IsOnline { get; protected set; }
        public ServerInfo ServerInfo { get; protected set; }
        public PlayerCollection Players { get; protected set; }
        #endregion

        #region Constructors
        public GameServer(string host, string name = DefaultName, string description = null)
            : this(NetworkSettings.GetDefault(host), name, description)
        { }
        public GameServer(string host, int port, string name = DefaultName, string description = null)
            : this(NetworkSettings.GetDefault(host, port), name, description)
        { }
        public GameServer(NetworkSettings settings, string name = DefaultName, string description = null)
        {
            if (settings == null)
                throw new ArgumentException("settings");
            this.Settings = settings;
            this.Name = name;
            this.Description = description;
        }
        #endregion

        #region Public Methods
        public void Update()
        {
            try
            {
                QueryClient client = new QueryClient();
                PacketCollection packets = client.QueryServer(this.Settings);
                string dataString = packets.GetDataString();
#if DEBUG
                Logger.Log(packets);
                Logger.LogData(packets);
                Logger.LogData(dataString);
#endif

                string[] data = Regex.Split(
                    dataString,
                    Encoding.ASCII.GetString(new byte[] { 00, 00, 01 }),
                    RegexOptions.Compiled);

                this.ServerInfo = ServerInfo.Parse(data[0]);
                this.Players = PlayerCollection.Parse(data[1]);
                ChangeOnlineState(true);
            }
            catch (SocketException ex)
            {
                ChangeOnlineState(false);
                Trace.WriteLine(
                    String.Format(CultureInfo.InvariantCulture, "{0}", ex.Message),
                    "Network Error");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(
                    String.Format(CultureInfo.InvariantCulture, "{0}", ex.Message), "General");
#if DEBUG
                Trace.WriteLine(ex.StackTrace, "Error");
#endif
            }
        }
        public override string ToString()
        {
            string format = "{0} ({1}:{2}) | State : {3} | Players : {4}/{5}";
            return String.Format(CultureInfo.InvariantCulture, format,
                this.Name,
                this.Settings.Host,
                this.Settings.RemotePort,
                this.IsOnline ? "ON" : "OFF",
                this.ServerInfo != null ? this.ServerInfo.NumPlayers.ToString() : "?",
                this.ServerInfo != null ? this.ServerInfo.MaxPlayers.ToString() : "?"
            );
        }
        #endregion

        #region Events System
        public event EventHandler<bool> OnlineStateChanged;
        protected virtual void OnOnlineStateChanged(bool isOnline)
        {
            EventHandler<bool> handler = OnlineStateChanged;
            if (handler != null)
            {
                handler(this, IsOnline);
            }
        }
        private void ChangeOnlineState(bool isOnline)
        {
            if (isOnline != this.IsOnline)
            {
                this.IsOnline = isOnline;
                OnOnlineStateChanged(isOnline);
            }
        }
        #endregion
    }
}
