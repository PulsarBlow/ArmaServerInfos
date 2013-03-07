using System.Diagnostics;

namespace ArmaServerInfo
{
    /// <summary>
    /// Player
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class Player
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
            : this(null)
        { }
        /// <summary>
        /// Creates a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name"></param>
        public Player(string name)
        {
            this.Name = name;
        }
    }
}
