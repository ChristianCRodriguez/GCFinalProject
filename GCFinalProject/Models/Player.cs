using System;
using System.Collections.Generic;

namespace GCFinalProject.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public double PlayerScore { get; set; }
        public string DifficultyLevel { get; set; }
        public DateTime? CreatedDay { get; set; }
        public DateTime? LastUpdatedDay { get; set; }
        public bool IsFirstTimeLoggingIn { get; set; }
        public string PlayerId { get; set; }

        public virtual AspNetUsers PlayerNavigation { get; set; }
    }
}
