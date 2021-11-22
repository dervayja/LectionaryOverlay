using System;
using System.Collections.Generic;
using System.Text;

namespace Lectionary.Model
{
    public interface IRepository
    {
        public List<LectionaryDate> Lectionary { get; set; }
        public Bible Bible { get; set; }
    }
}
