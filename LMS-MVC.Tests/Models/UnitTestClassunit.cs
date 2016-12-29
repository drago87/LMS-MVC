using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LMS_MVC.Models;
using System.Collections.Generic;

namespace LMS_MVC.Tests.Models
{
    [TestClass]
    public class UnitTestClassUnit
    {
        [TestMethod]
        public void GetSchema_5days_6lessons_Only_3Within_5days()
        {
            ClassUnit _class = new ClassUnit();

            var _from = DateTime.Now;
            var _to = DateTime.Now.AddDays(5);


            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddHours(1), StopTime = DateTime.Now.AddHours(2) });
            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddHours(2), StopTime = DateTime.Now.AddHours(3) });
            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddHours(3), StopTime = DateTime.Now.AddHours(4) });

            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddDays(7).AddHours(1), StopTime = DateTime.Now.AddDays(7).AddHours(2) });
            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddDays(7).AddHours(2), StopTime = DateTime.Now.AddDays(7).AddHours(3) });
            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddDays(7).AddHours(3), StopTime = DateTime.Now.AddDays(7).AddHours(4) });

            List<Lesson> schema = _class.GetSchema(_from, _to);

            Assert.AreEqual(3, schema.Count);

        }

        [TestMethod]
        public void GetSchema_10days_6lessons_All_Within_10days()
        {
            ClassUnit _class = new ClassUnit();

            var _from = DateTime.Now;
            var _to = DateTime.Now.AddDays(10);


            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddHours(1), StopTime = DateTime.Now.AddHours(2) });
            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddHours(2), StopTime = DateTime.Now.AddHours(3) });
            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddHours(3), StopTime = DateTime.Now.AddHours(4) });

            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddDays(7).AddHours(1), StopTime = DateTime.Now.AddDays(7).AddHours(2) });
            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddDays(7).AddHours(2), StopTime = DateTime.Now.AddDays(7).AddHours(3) });
            _class.Schema.Add(new Lesson { StartTime = DateTime.Now.AddDays(7).AddHours(3), StopTime = DateTime.Now.AddDays(7).AddHours(4) });

            List<Lesson> schema = _class.GetSchema(_from, _to);

            Assert.AreEqual(6, schema.Count);

        }

    }
}
