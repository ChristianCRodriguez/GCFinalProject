using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCFinalProject.Models
{
    public class PlayerStatus
    {
        public List<Player> Leaderboard { get; set; }
        public Avatar CurrentAvatar { get; set; }
        public Player CurrentPlayer { get; set; }
    }
}
