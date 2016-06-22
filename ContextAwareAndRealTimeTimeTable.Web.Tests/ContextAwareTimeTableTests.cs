using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContextAwareAndRealTimeTimeTable.Web.Tests
{
    [TestClass]
    public class ContextAwareTimeTableTests
    {
        [TestMethod]
        public void ConfirmCurrentTimeInHoursIsBetweenStartTimeAndEndTimeOfAnActivity()
        {
            //Arrange
            int currentHours = DateTime.Now.Hour;
            int startHour = 11;
            int endHour = 14;
            var actual = true;
            bool expected;
            //Act
            if (startHour <= currentHours && currentHours <= endHour)
            {
                expected = true;
            }
            else
            {
                expected = false;
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConfirmCurrentTimeInHoursIsNotBetweenStartTimeAndEndTimeOfAnActivity()
        {
            //Arrange
            int currentHours = DateTime.Now.Hour;
            int startHour = 12;
            int endHour = 14;
            var actual = true;
            bool expected;
            //Act
            if (startHour <= currentHours && currentHours <= endHour)
            {
                expected = true;
            }
            else
            {
                expected = false;
            }

            //Assert
            Assert.AreNotEqual(expected, actual);
        }

       // [TestMethod]
        
    }
}
