using LawInOrder.Timesheet.Web.DAL;
using LawInOrder.Timesheet.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LawInOrder.Timesheet.Web.Tests.DAL
{
    [TestClass]
    public class TimesheetRepositoryTest
    {
        [TestMethod]
        public void AddTime_Success()
        {
            // Setup
            TimesheetContext db = new TimesheetContext();
            User user = db.Users.First();
            int maxTimeId = db.Times.Select(t => t.Id).Max();
            Time time = new Time() { Date = DateTime.Today, HoursWorked = 5, UserId = user.Id };

            // Test action
            TimesheetRepository repository = new TimesheetRepository();
            repository.AddTime(time);

            // Assertions
            Time newTime = db.Times.First(t => t.Id > maxTimeId);
            Assert.IsNotNull(newTime);
            Assert.AreEqual(time.Date, newTime.Date);

            // Cleanup
            db.Times.Remove(newTime);
        }

        [TestMethod]
        public void GetSubordinatesForUser_Success()
        {
            // Setup
            TimesheetContext db = new TimesheetContext();
            User user = db.Users.First(u => u.ManagerId.HasValue);

            // Test action
            TimesheetRepository repository = new TimesheetRepository();
            var subordinates = repository.GetSubordinatesForUser(user.Id);

            // Assertions
            Assert.IsNotNull(subordinates);
            Assert.IsTrue(subordinates.Any());
        }

        [TestMethod]
        public void GetTimesForUser_Success()
        {
            // Setup
            TimesheetContext db = new TimesheetContext();
            User user = db.Times.First().User;

            // Test action
            TimesheetRepository repository = new TimesheetRepository();
            var times = repository.GetTimesForUser(user.Id);

            // Assertions
            Assert.IsNotNull(times);
            Assert.IsTrue(times.Any());
        }

        [TestMethod]
        public void GetUser_Success()
        {
            // Setup
            TimesheetContext db = new TimesheetContext();
            User user = db.Users.First();

            // Test action
            TimesheetRepository repository = new TimesheetRepository();
            var authenticatedUser = repository.GetUser(user.Login, user.Password);

            // Assertions
            Assert.IsNotNull(authenticatedUser);
            Assert.AreEqual(user.Login, authenticatedUser.Login);
        }
    }
}
