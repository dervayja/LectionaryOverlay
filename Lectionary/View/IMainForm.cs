using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Lectionary.View
{
    interface IMainForm
    {
        public void PaintAll(object sender, EventArgs e);

        public int PositionX { get; }
        public int PositionY { get; }
        public string Date { get; set; }
        public string FeastsAndSaints { get; set; }
        public List<string> ReadingTitles { get; set; }
        public List<string> Readings { get; set; }

    }
}
