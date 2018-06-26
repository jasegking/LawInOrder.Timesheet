using LawInOrder.Timesheet.Web.DAL;
using LawInOrder.Timesheet.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LawInOrder.Timesheet.Web.Managers
{
    public class TimesheetRepository
    {
        TimesheetContext db = new TimesheetContext();

        /// <summary>
        /// Get all times associated with the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Time> GetTimesForUser(int userId)
        {
            return db.Times.Where(t => t.UserId == (int)userId).Include(t => t.User);
        }

        /// <summary>
        /// Get subordinates for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<User> GetSubordinatesForUser(int userId)
        {
            return db.Users.Where(u => u.ManagerId == userId);
        }

        /// <summary>
        /// Add a time entry to the database
        /// </summary>
        /// <param name="time"></param>
        public void AddTime(Time time)
        {
            db.Times.Add(time);
            db.SaveChanges();
        }

        /// <summary>
        /// Return the user associated with the userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUser(int userId)
        {
            return db.Users.Include(u => u.Manager).Single(u => u.Id == userId);
        }

        /// <summary>
        /// Return the user associated with the login credentials
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetUser(string login, string password)
        {
            return db.Users.Single(u => u.Login == login && u.Password == password);
        }
    }
}