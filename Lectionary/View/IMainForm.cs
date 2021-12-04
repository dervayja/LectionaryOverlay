using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Lectionary.View
{
    interface IMainForm
    {
        void PaintAll(object sender, EventArgs e);

        int PositionX { get; }
        int PositionY { get; }
        string Date { get; set; }
        string FeastsAndSaints { get; set; }
        List<string> ReadingTitles { get; set; }
        List<string> Readings { get; set; }

    }
}
