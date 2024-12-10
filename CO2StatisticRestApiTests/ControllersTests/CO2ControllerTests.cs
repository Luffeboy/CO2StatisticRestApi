//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using CO2StatisticRestApi.Controllers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CO2StatisticRestApi;
//using CO2DatabaseLib;
//using CO2DatabaseLib.Models;

//namespace CO2StatisticRestApiTests
//{
//    [TestClass()]
//    public class CO2ControllerTests
//    {
//        CO2Controller _controller;

//        [TestMethod()]
//        public void GetByIdTest()
//        {

//            _controller = new CO2Controller();

//            Assert.IsTrue(_controller.Get(1).Count() > 0);

//            var startTime = new DateTime(2024, 4, 4);
//            var endTime = new DateTime(2025, 1, 1);
//            var data = _controller.Get(1, startTime, endTime);

//            foreach(var d in data)
//            {
//                Assert.IsTrue(d.MeasurementTime >= startTime);

//            }

//            data = _controller.Get(1, endTime: endTime);
//            foreach (var d in data)
//            {
//                Assert.IsTrue(d.MeasurementTime <= endTime);
//            }

//            data = _controller.Get(1, startTime, endTime);
//            foreach (var d in data)
//            {
//                Assert.IsTrue(d.MeasurementTime <= endTime && d.MeasurementTime >= startTime);
//            }
//        }

//        [TestMethod()]
//        public void ChangeWarningValueTest()
//        {
//            SensorRepository repo = new SensorRepository();
//            int warningValue = 1000;
//            Sensor sensor = repo.Create("test", warningValue);
//            Assert.AreEqual(warningValue, sensor.WarningValue);
//            int newWarningValue = 2000;
//            repo.ChangeWarningValue(sensor.Id, newWarningValue);
//            Assert.AreEqual(newWarningValue, sensor.WarningValue);

         
//        }

       
//    }
//}