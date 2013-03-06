using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ArmaServerInfo
{
    /// <summary>
    /// A collection of <see cref="GameServer"/>
    /// </summary>
    public class GameServerCollection : Dictionary<string, GameServer>
    {
        #region Constructors
        public GameServerCollection() : base() { }
        public GameServerCollection(IDictionary<string, GameServer> dictionary) : base(dictionary) { }
        public GameServerCollection(IEqualityComparer<string> comparer) : base(comparer) { }
        public GameServerCollection(int capacity) : base(capacity) { }
        public GameServerCollection(IDictionary<string, GameServer> dictionary, IEqualityComparer<string> comparer) : base(dictionary, comparer) { }
        protected GameServerCollection(SerializationInfo info, StreamingContext context) : base(info, context) { }
        #endregion

        /// <summary>
        /// Update the current server data
        /// </summary>
        public void Update()
        {
            if (this.Count == 0)
                return;
            foreach (KeyValuePair<string, GameServer> pair in this)
            {
                pair.Value.Update();
            }
        }
    }
}
