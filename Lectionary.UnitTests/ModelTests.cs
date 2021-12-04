using Lectionary.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Lectionary.UnitTests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void CanInstantiateAllDays()
        {
            var model = new Model.Model();
            bool pass = true;
            DateTime StartDate = new DateTime(2017, 11, 27);
            DateTime EndDate = new DateTime(2034, 11, 27);
            foreach (DateTime day in EachDay(StartDate, EndDate))
            {
                try
                {
                    model.UpdateModel(day);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    pass = false;
                    throw;
                }
                if (model.Today.LectionaryEntry.Readings.Count <= 0 && model.Today.LectionaryEntry.FeastsAndSaints.Length > 10)
                {
                    pass = false;
                    break;
                }
                else
                {
                    pass = true;
                }
            }
            Assert.IsTrue(pass);
        }
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }


}
