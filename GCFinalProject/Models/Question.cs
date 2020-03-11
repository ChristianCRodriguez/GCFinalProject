using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCFinalProject.Models
{
    public class Question
    {
        public string id { get; set; }
        public string question { get; set; }
        public List<string> choice { get; set; }
        public int correct_choice { get; set; }
        public string instruction { get; set; }
        public string category { get; set; }
        public string topic { get; set; }
        public string difficulty { get; set; }
    }
}
