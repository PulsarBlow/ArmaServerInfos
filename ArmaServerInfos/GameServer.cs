using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace ArmaServerInfo
{
    /// <summary>
    /// Game server entity and its associated data
    /// </summary>
    public class GameServer
    {
        #region Members & Properties
        private const string DefaultName = "Unknown server";
        /// <summary>
        /// Network Settings (Host, Port) used by the <see cref="QueryClient"/> to connect to the server
        /// </summary>
        public NetworkSettings Settings { get; protected set; }
        /// <summary>
        /// Custom name
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Custom description
        /// </summary>
        public string Description { get; protected set; }
        /// <summary>
        /// Gets the server online state.
        /// </summary>
        public bool IsOnline { get; protected set; }
        /// <summary>
        /// Contains all the data relative to the server features.
        /// </summary>
        public ServerInfo ServerInfo { get; protected set; }
        /// <summary>
        /// The list of player actually connected and playing on the server
        /// </summary>
        public PlayerCollection Players { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of the <see cref="GameServer"/> class.
        /// </summary>
        /// <param name="host">The IP address of the server</param>
        /// <param name="name">The custom name you want to give to this server</param>
        /// <param name="description">The custom description you want to give to this server</param>
        public GameServer(string host, string name = DefaultName, string description = null)
            : this(NetworkSettings.GetDefault(host), name, description)
        { }
        /// <summary>
        /// Creates a new instance of the <see cref="GameServer"/> class.
        /// </summary>
        /// <param name="host">The IP address of the server</param>
        /// <param name="port">The port number of the server</param>
        /// <param name="name">The custom name you want to give to this server</param>
        /// <param name="description">The custom description you want to give to this server</param>
        public GameServer(string host, int port, string name = DefaultName, string description = null)
            : this(NetworkSettings.GetDefault(host, port), name, description)
        { }
        /// <summary>
        /// Creates a new instance of the <see cref="GameServer"/> class.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
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
        /// <summary>
        /// Occurs when the server online state changes while update.
        /// </summary>
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
