using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lectionary.Model
{
    public class Bible
    {
        public string version { get; set; }
        public IList<Books> books { get; set; }
    }

    public class Verses
    {
        public string text { get; set; }
        public int num { get; set; }

    }
    public class Chapters
    {
        public IList<Verses> verses { get; set; }
        public int num { get; set; }

    }
    public class Books
    {
        public string name { get; set; }
        public IList<Chapters> chapters { get; set; }

    }
}
