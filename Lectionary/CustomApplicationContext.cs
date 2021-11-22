using System;
using System.Windows.Forms;
using Lectionary.View;
using Lectionary.Model;
using Lectionary.Presenter;

namespace Lectionary
{
    public class CustomApplicationContext : ApplicationContext
    {
        private SystemTrayIcon theIcon;
        private MainForm theForm;
        private Model.Model theModel;

        public event EventHandler PaintForm;

        public CustomApplicationContext()
        {
            //Properties.Settings.Default.Reload();
            theIcon = new SystemTrayIcon();
            theForm = new MainForm();
            theForm.Show();
            theModel = new Model.Model();
            AppDomain.CurrentDomain.ProcessExit += Exit;

            theIcon.OnSettingsChange += TheIcon_OnSettingsChange;
            theModel.OnDataChanged += TheModel_OnDataChanged;
            theModel.OnDataChanged += theForm.PaintAll;
            theIcon.OnSettingsChange += theForm.PaintAll;
            theIcon.OnMoveWindow += theForm.EnableWindowMovement;

            theModel.UpdateModel();
            theForm.Icon = Resources.AppIcon;
            theForm.ShowIcon = true;
        }

        private void TheModel_OnDataChanged(object sender, EventArgs e)
        {
            theForm.Date = theModel.Today.Day.DayOfWeek.ToString() + " " + theModel.Today.Day.Month.ToString() + "/" + theModel.Today.Day.Day.ToString() + "/" + theModel.Today.Day.Year.ToString();
            theForm.FeastsAndSaints = theModel.Today.FeastsAndSaints;
            LoadReadings();
            PaintForm?.Invoke(this, EventArgs.Empty);
        }

        private void TheIcon_OnSettingsChange(object sender, EventArgs e)
        {
            PaintForm?.Invoke(this, EventArgs.Empty);
            //theForm.PaintAll();
        }

        private void LoadReadings()
        {
            theForm.ReadingTitles.Clear();
            theForm.Readings.Clear();
            for (int i = 0; i < theModel.Today.ReadingList.Count; i++)
            {
                string readingText = "";
                for (int j = 0; j < theModel.Today.ReadingList[i].Verses.Count; j++)
                {
                    readingText += theModel.Today.ReadingList[i].Verses[j].Text + " ";
                }
                string readingTitle;
                if (theModel.Today.LectionaryEntry.Readings[i].StartChapter == theModel.Today.LectionaryEntry.Readings[i].EndChapter)
                {
                    readingTitle = theModel.Today.LectionaryEntry.Readings[i].Book.ToString() + " " + theModel.Today.LectionaryEntry.Readings[i].StartChapter.ToString() + ":" + theModel.Today.LectionaryEntry.Readings[i].StartVerse.ToString() + "-" + theModel.Today.LectionaryEntry.Readings[i].EndVerse.ToString();
                }
                else
                {
                    readingTitle = theModel.Today.LectionaryEntry.Readings[i].Book.ToString() + " " + theModel.Today.LectionaryEntry.Readings[i].StartChapter.ToString() + ":" + theModel.Today.LectionaryEntry.Readings[i].StartVerse.ToString() + "-" + theModel.Today.LectionaryEntry.Readings[i].EndChapter.ToString() + ":" + theModel.Today.LectionaryEntry.Readings[i].EndVerse.ToString();
                }
                //readingTitle = theModel.Today.LectionaryEntry.Readings[i].Book.ToString() + " " + theModel.Today.LectionaryEntry.Readings[i].StartChapter.ToString() + ":" + theModel.Today.LectionaryEntry.Readings[i].StartVerse.ToString() + " - " + theModel.Today.LectionaryEntry.Readings[i].EndChapter.ToString() + ":" + theModel.Today.LectionaryEntry.Readings[i].EndVerse.ToString();
                theForm.ReadingTitles.Add(readingTitle);
                theForm.Readings.Add(readingText);
            }
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            theIcon.notifyIcon.Visible = false;

            Application.Exit();
        }
    }


}
