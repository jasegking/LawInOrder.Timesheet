using LawInOrder.Timesheet.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LawInOrder.Timesheet.Web.DAL
{
    public class TimesheetInitializer : DropCreateDatabaseIfModelChanges<TimesheetContext>
    {
        /// <summary>
        /// Pre-populate the database with some basic testing data
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(TimesheetContext context)
        {
            var users = new List<User>()
            {
                new User() { Id = 1, DisplayName = "Jason King", Login = "jason", Password = "password", Email = "jase.g.king@gmail.com" },
                new User() { Id = 2, DisplayName = "CEO", Login = "manager", Password = "password", Email = "jase.g.king@gmail.com" },
                new User() { Id = 3, DisplayName = "Junior Developer from India", Login = "employee", Password = "password", Email = "jase.g.king@gmail.com" }
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            context.Users.Single(u => u.Id == 1).ManagerId = 2;
            context.Users.Single(u => u.Id == 3).ManagerId = 1;

            var times = new List<Time>()
            {
                new Time() { User = context.Users.Single(u => u.Login == "jason"), Date = DateTime.Today, HoursWorked = 8 },
            };

            times.ForEach(t => context.Times.Add(t));
            context.SaveChanges();
        }
    }
}