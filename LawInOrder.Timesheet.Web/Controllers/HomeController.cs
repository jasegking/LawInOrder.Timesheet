using LawInOrder.Timesheet.Web.Attributes;
using LawInOrder.Timesheet.Web.DAL;
using LawInOrder.Timesheet.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace LawInOrder.Timesheet.Web.Controllers
{
    //[LocalAuthorizationFilter]
    public class HomeController : Controller
    {
        private TimesheetContext db = new TimesheetContext();

        /// <summary>
        /// Default view for the application displaying a report of time entered for self and subordinates
        /// </summary>
        /// <returns></returns>
        //[LocalAuthorizationFilter]
        public ActionResult Index()
        {
            List<Time> times = new List<Time>();

            // Set the User object
            object userId = HttpContext.Cache["LoggedInUserId"];
            if (userId != null)
            {
                times = GetTimes((int)userId);
            }

             return View(times.OrderBy(t => t.User.DisplayName).ThenBy(t => t.Date));
        }

        /// <summary>
        /// Recursive function to get timesheet for self and all subordinates
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<Time> GetTimes(int userId)
        {
            List<Time> times = new List<Time>();

            // Get the current users timesheet
            times.AddRange(db.Times.Where(t => t.UserId == (int)userId).Include(t => t.User));

            // Get all subordinates timesheets
            db.Users.Where(u => u.ManagerId == userId)
                    .ToList()
                    .ForEach(u => times.AddRange(GetTimes(u.Id)));

            return times;
        }

        /// <summary>
        /// Set up the page to allow the user to add a new timesheet entry
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        public ActionResult AddTime()
        {
            Time time = new Time();

            // Set the User object
            object userId = HttpContext.Cache["LoggedInUserId"];
            if (userId != null)
            {
                time.UserId = (int)userId;
            }

            return View(time);
        }

        /// <summary>
        /// Save the time entry for the current user
        /// </summary>
        /// <param name="time">Time object to save to the database</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddTime(Time time)
        {
            if (ModelState.IsValid)
            {
                db.Times.Add(time);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(time);
        }

        /// <summary>
        /// Set up the page to allow the user to add a new timesheet entry
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
             return View();
        }

        /// <summary>
        /// Save the time entry for the current user
        /// </summary>
        /// <param name="user">User object containing the user name and password</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                user = db.Users.Single(u => u.Login == user.Login && u.Password == user.Password);
                if (user != null)
                {
                    HttpContext.Cache["LoggedInUserId"] = user.Id;
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}