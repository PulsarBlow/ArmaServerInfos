using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace ArmaServerInfo
{
    /// <summary>
    /// A collection of <see cref="Player"/> instances
    /// </summary>
    public class PlayerCollection : List<Player>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="PlayerCollection"/> class.
        /// </summary>
        public PlayerCollection()
            : base()
        { }
        /// <summary>
        /// Creates a new instance of the <see cref="PlayerCollection"/> class.
        /// </summary>
        /// <param name="collection"></param>
        public PlayerCollection(IEnumerable<Player> collection)
            : base(collection)
        { }        
        /// <summary>
        /// Parse the data string and returns a new <see cref="PlayerCollection"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static PlayerCollection Parse(string data)
        {
            if (String.IsNullOrWhiteSpace(data))
                return new PlayerCollection();

            string[] dataBlocks = Regex.Split(data, Encoding.ASCII.GetString(new byte[] { 00, 00 }), RegexOptions.Compiled);
            if (dataBlocks == null || dataBlocks.Length == 0)
                return new PlayerCollection();

            var players = dataBlocks[1].Split('\0').Distinct();
            if (players == null || players.Count() == 0)
                return new PlayerCollection();

            PlayerCollection collection = new PlayerCollection(
                from s in players                
                select new Player(s)
            );            
            return collection;
        }
    }
}
