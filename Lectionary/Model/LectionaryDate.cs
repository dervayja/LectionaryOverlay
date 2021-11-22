using System;
using System.Collections.Generic;
using System.Text;

namespace Lectionary.Model
{
    public class LectionaryDate
    {
        public DateTime Day { get; set; }
        public string FeastsAndSaints { get; set; }
        public List<Reading> Readings { get; set; } = new List<Reading>();
        
    }

    public class Reading
    {
        public string Book { get; set; }
        public int StartChapter { get; set; }
        public int EndChapter { get; set; }
        public int StartVerse { get; set; }
        public int EndVerse { get; set; }
    }
}
