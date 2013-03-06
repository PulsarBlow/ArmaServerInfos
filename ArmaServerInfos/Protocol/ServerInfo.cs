using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmaServerInfo
{
    /// <summary>
    /// Smart Container for server info data
    /// </summary>
    [DebuggerDisplay("Players : {NumPlayers}/{MaxPlayers}")]
    public class ServerInfo
    {
        #region Properties
        public string GameVersion { get; set; }
        public string HostName { get; set; }
        public string MapName { get; set; }
        public string GameType { get; set; }
        public int NumPlayers { get; set; }
        public int NumTeam { get; set; }
        public int MaxPlayers { get; set; }
        public string GameMode { get; set; }
        public string TimeLimit { get; set; }
        public bool Password { get; set; }
        public string CurrentVersion { get; set; }
        public string RequiredVersion { get; set; }
        public string Mod { get; set; }
        public bool BattleEye { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<string> Players { get; set; }
        #endregion

        public static ServerInfo Parse(string data)
        {
            ServerInfo info = new ServerInfo();
            string[] parts = data.Split('\0');
            Dictionary<string, string> values = new Dictionary<string, string>();
            for (int i = 0; i < parts.Length; i++)
            {
                if ((i & 1) == 0 && !values.ContainsKey(parts[i]) && (i+1) < parts.Length)
                    values.Add(parts[i], parts[i + 1]);
            }

            info.GameVersion = GetValueByKey("gamever", values);
            info.HostName = GetValueByKey("hostname", values);
            info.MapName = GetValueByKey("mapname", values);
            info.GameType = GetValueByKey("gametype", values);
            info.NumPlayers = ParseInt(GetValueByKey("numplayers", values));
            info.NumTeam = ParseInt(GetValueByKey("numteams", values));
            info.MaxPlayers = ParseInt(GetValueByKey("maxplayers", values));
            info.GameMode = GetValueByKey("gamemode", values);
            info.TimeLimit = GetValueByKey("timelimit", values);
            info.Password = ParseBoolean(GetValueByKey("password", values));
            info.CurrentVersion = GetValueByKey("currentVersion", values);
            info.RequiredVersion = GetValueByKey("requiredVersion", values);
            info.Mod = GetValueByKey("mod", values);
            info.BattleEye = ParseBoolean(GetValueByKey("sv_battleye", values));
            info.Longitude = ParseDouble(GetValueByKey("lng", values));
            info.Latitude = ParseDouble(GetValueByKey("lat", values));

            return info;
        }
        private static int ParseInt(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return 0;
            int parsedValue = 0;
            Int32.TryParse(value, out parsedValue);
            return parsedValue;
        }
        private static double ParseDouble(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return 0;
            double parsedValue = 0;
            Double.TryParse(value, out parsedValue);
            return parsedValue;
        }
        private static bool ParseBoolean(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return false;
            if (value == "1" || value.ToLowerInvariant() == "true")
                return true;
            return false;
        }
        private static string GetValueByKey(string key, Dictionary<string, string> values)
        {
            if(values.ContainsKey(key))
                return values[key];
            return null;
        }
    }
}
