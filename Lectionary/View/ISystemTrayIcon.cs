using System;
using System.Collections.Generic;
using System.Text;

namespace Lectionary.View
{
    interface ISystemTrayIcon
    {
        event EventHandler OnSettingsChange;
    }
}
