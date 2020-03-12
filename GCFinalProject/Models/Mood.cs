using System;
using System.Collections.Generic;

namespace GCFinalProject.Models
{
    public partial class Mood
    {
        public int MoodId { get; set; }
        public int MinEnergy { get; set; }
        public int MaxEnergy { get; set; }
        public byte[] MoodImage { get; set; }
        public string Description { get; set; }
    }
}
