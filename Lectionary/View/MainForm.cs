using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Lectionary.View
{
    public partial class MainForm : Form, IMainForm
    {


        public MainForm()
        {
            InitializeComponent();
            MENUSTRIP_READINGS.BackColor = Color.Transparent;
            Point startPoint = new Point(Properties.Settings.Default.XPosition, Properties.Settings.Default.YPosition);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = startPoint;
            this.Icon = Resources.AppIcon;
        }

        public int PositionX { get; }
        public int PositionY { get; }
        public string Date { get; set; }
        public string FeastsAndSaints { get; set; }
        public List<string> ReadingTitles { get; set; } = new List<string>();
        public List<string> Readings { get; set; } = new List<string>();


        public void PaintAll(object sender, EventArgs e)
        {
            this.BackColor = Properties.Settings.Default.BackgroundColor;
            this.LABEL_DATE.Text = Date;
            LABEL_FEASTS_AND_SAINTS.Text = FeastsAndSaints;
            LABEL_FEASTS_AND_SAINTS.Cursor = (LABEL_FEASTS_AND_SAINTS.Height > 60) ? Cursors.SizeNS : Cursors.Default;
            UpdateFonts();
            UpdateReadingTitles();
            InitializeNewReadings();
        }

        public void EnableWindowMovement(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void UpdateFonts()
        {
            Font regularFont = new Font("Segoe UI", 10, FontStyle.Regular);
            Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
            LABEL_DATE.Font = regularFont;
            LABEL_FEASTS_AND_SAINTS.Font = regularFont;
            LABEL_READING.Font = regularFont;
            LABEL_TITLE_FEASTSANDSAINTS.Font = boldFont;
            LABEL_TITLE_READING.Font = boldFont;
            UpdateReadingTitles();
            InitializeNewReadings();
        }

        public void UpdateReadingTitles()
        {
            MENUSTRIP_READINGS.Items.Clear();
            foreach (string title in ReadingTitles)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = title;
                Font menuFont = new Font(Properties.Settings.Default.TextFont.FontFamily, 10, FontStyle.Regular);
                item.Font = menuFont;
                
                MENUSTRIP_READINGS.Items.Add(item);
            }

            Cursor menuCursor = (MENUSTRIP_READINGS.Height > 50) ? Cursors.SizeNS : Cursors.Default;
            MENUSTRIP_READINGS.Cursor = menuCursor;
            foreach (ToolStripMenuItem item in MENUSTRIP_READINGS.Items)
            {
                item.MouseEnter += (obj, arg) => MENUSTRIP_READINGS.Cursor = Cursors.Default;
                item.MouseLeave += (obj, arg) => MENUSTRIP_READINGS.Cursor = menuCursor;
            }


            InitializeNewReadings();
        }

        public void InitializeNewReadings()
        {
            Font boldFont = new Font(MENUSTRIP_READINGS.Items[0].Font, FontStyle.Bold);
            MENUSTRIP_READINGS.Items[0].Font = boldFont;
            LABEL_READING.Text = Readings[0];
            LABEL_READING.Cursor = (LABEL_READING.Height > 220) ? Cursors.SizeNS : Cursors.Default;
        }

        public void UpdateSize(string size)
        {
            throw new NotImplementedException();
        }

        private void TABLELAYOUTPANEL_MAIN_MouseClick(object sender, MouseEventArgs e)
        {
            Properties.Settings.Default.XPosition = this.Location.X;
            Properties.Settings.Default.YPosition = this.Location.Y;
            Properties.Settings.Default.Save();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void MENUSTRIP_READINGS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set all to regular style font then make selected item bold font
            var item = e.ClickedItem;
            var regularFont = new Font(Properties.Settings.Default.TextFont.FontFamily, 10, FontStyle.Regular);
            var boldFont = new Font(Properties.Settings.Default.TextFont.FontFamily, 10, FontStyle.Bold);
            foreach (ToolStripItem title in item.Owner.Items)
            {
                title.Font = regularFont;
            }

            item.Font = boldFont;

            int index = item.Owner.Items.IndexOf(item);
            LABEL_READING.Text = Readings[index];
            PANEL_READING_FRONT.VerticalScroll.Value = 0;
            LABEL_READING.Cursor = (LABEL_READING.Height > 220) ? Cursors.SizeNS : Cursors.Default;
        }

        private void LABEL_READING_Click(object sender, EventArgs e)
        {

        }
    }
}
