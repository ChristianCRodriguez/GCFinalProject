using System;
using System.Collections.Generic;

namespace GCFinalProject.Models
{
    public partial class Avatar
    {
        public int AvatarId { get; set; }
        public DateTime ExpireDate { get; set; }
        public int AvatarEnergy { get; set; }
        public string AvatarName { get; set; }
        public bool IsActive { get; set; }
        public string PlayerId { get; set; }
        public int MoodId { get; set; }
        public DateTime LastFeedDate { get; set; }

        public virtual Mood Mood { get; set; }
        public virtual AspNetUsers Player { get; set; }
    }
}
