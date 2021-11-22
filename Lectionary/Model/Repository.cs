using System;
using System.Collections.Generic;
using System.Text;
using Lectionary.Model;
using System.Text.Json;

namespace Lectionary.Model
{
    public class Repository : IRepository
    {
        public List<LectionaryDate> Lectionary { get; set; } = new List<LectionaryDate>();
        public Bible Bible { get; set; } = new Bible(); 

        public Repository()
        {
            Initialize();
        }

        private void Initialize()
        {
            Lectionary = JsonSerializer.Deserialize<List<LectionaryDate>>(Resources.lectionary);
            Bible = JsonSerializer.Deserialize<Bible>(Resources.NKJV);
        }
    }
}
