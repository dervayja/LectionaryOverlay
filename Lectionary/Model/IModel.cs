using System;
using System.Collections.Generic;
using System.Text;

namespace Lectionary.Model
{
    interface IModel
    {
        public event EventHandler OnDataChanged;
    }
}
