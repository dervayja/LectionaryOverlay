using System;
using System.Collections.Generic;
using System.Text;

namespace Lectionary.Model
{
    public class MainTimer : IMainTimer
    {
        public event EventHandler OnTimerTick;
        private System.Timers.Timer mainTimer = new System.Timers.Timer(2000);

        public MainTimer()
        {
            mainTimer.Elapsed += MainTimer_Elapsed;
            mainTimer.Enabled = true;
            mainTimer.AutoReset = true;
            mainTimer.Start();
        }

        private void MainTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            OnTimerTick?.Invoke(this, EventArgs.Empty);
        }
    }
}
