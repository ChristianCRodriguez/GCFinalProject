using System;
using System.Collections.Generic;

namespace GCFinalProject.Models
{
    public partial class Player
    {
        public int PlayerId { get; set; }
        public string PlayerEmail { get; set; }
        public int? PlayerScore { get; set; }
        public string DifficultyLevel { get; set; }
        public DateTime? CreatedDay { get; set; }
        public DateTime? LastUpdatedDay { get; set; }
    }
}
