using System.Diagnostics;

namespace ArmaServerInfo
{
    [DebuggerDisplay("{Name}, Team: {Team}, Score: {Score}")]
    public class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
        public int Score { get; set; }

        public Player()
        { }
        public Player(string name, string team = null, int score = 0)
            : this()
        {
            this.Name = name;
            this.Team = team;
            this.Score = score;
        }
    }
}
