using System;
using System.Collections.Generic;

namespace GCFinalProject.Models
{
    public partial class Mood
    {
        public Mood()
        {
            Avatar = new HashSet<Avatar>();
        }

        public int MoodId { get; set; }
        public int MinEnergy { get; set; }
        public int MaxEnergy { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Avatar> Avatar { get; set; }
    }
}
