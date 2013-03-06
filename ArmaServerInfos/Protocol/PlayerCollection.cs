using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace ArmaServerInfo
{
    public class PlayerCollection : List<Player>
    {
        public PlayerCollection()
            : base()
        { }
        public PlayerCollection(int capacity)
            : base(capacity)
        { }
        public PlayerCollection(IEnumerable<Player> collection)
            : base(collection)
        { }        
        public static PlayerCollection Parse(string data)
        {
            if (String.IsNullOrWhiteSpace(data))
                return new PlayerCollection();

            string[] subData = Regex.Split(data, Encoding.ASCII.GetString(new byte[] { 00, 00 }), RegexOptions.Compiled);
            var players = subData[1].Split('\0').Distinct();
            PlayerCollection collection = new PlayerCollection(
                from s in players                
                select new Player(s)
            );            
            return collection;
        }
    }
}
