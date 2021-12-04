using System;
using System.Collections.Generic;
using System.Text;

namespace Lectionary.Model
{
    public interface IRepository
    {
        List<LectionaryDate> Lectionary { get; set; }
        Bible Bible { get; set; }
    }
}
