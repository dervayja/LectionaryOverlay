using System;
using System.Collections.Generic;
using System.Text;

namespace Lectionary.Model
{
    public class Model : IModel
    {
        public event EventHandler OnDataChanged;
        public DailyData Today { get; set; }

        private Repository repo = new Repository();
        private MainTimer timer = new MainTimer();
        public Model()
        {
            // Initilize today's lectionary
            LectionaryDate todaysLectionary = repo.Lectionary.Find(x => x.Day.Date == DateTime.Now.Date);
            Today = new DailyData(todaysLectionary, repo.Bible);
            timer.OnTimerTick += MainTimer_Ticked;
            UpdateModel();
        }

        private void MainTimer_Ticked(object sender, EventArgs e)
        {
            
            // Update data and raise event if new day
            if (Today.Day.Date != DateTime.Now.Date)
            {
                UpdateModel();
            }
            
        }

        public void UpdateModel()
        {
            LectionaryDate todaysLectionary = repo.Lectionary.Find(x => x.Day.Date == DateTime.Now.Date);
            Today = new DailyData(todaysLectionary, repo.Bible);
            OnDataChanged?.Invoke(this, EventArgs.Empty);
        }


    }
}
