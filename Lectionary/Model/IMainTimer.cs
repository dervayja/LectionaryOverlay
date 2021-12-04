using System;
using System.Collections.Generic;
using System.Text;

namespace Lectionary.Model
{
    public interface IMainTimer
    {
        event EventHandler OnTimerTick;
    }
}
