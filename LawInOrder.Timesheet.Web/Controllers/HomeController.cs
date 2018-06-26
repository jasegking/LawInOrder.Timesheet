using LawInOrder.Timesheet.Web.Attributes;
using LawInOrder.Timesheet.Web.DAL;
using LawInOrder.Timesheet.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LawInOrder.Timesheet.Web.Controllers
{
    [LocalAuthorizationFilter]
    public class HomeController : Controller
    {
        private TimesheetRepository repository = new TimesheetRepository();

        /// <summary>
        /// Default view for the application displaying a report of time entered for self and subordinates
        /// </summary>
        /// <returns></returns>
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
        /// Set up the page to allow the user to add a new timesheet entry
        /// </summary>
        /// <returns></returns>
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
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddTime(Time time)
        {
            if (ModelState.IsValid)
            {
                repository.AddTime(time);
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
                user = repository.GetUser(user.Login, user.Password);
                if (user != null)
                {
                    HttpContext.Cache["LoggedInUserId"] = user.Id;
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        #region Private methods

        /// <summary>
        /// Recursive function to get timesheet for self and all subordinates
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<Time> GetTimes(int userId)
        {
            List<Time> times = new List<Time>();

            // Get the current users timesheet
            times.AddRange(repository.GetTimesForUser(userId));

            // Get all subordinates timesheets
            repository.GetSubordinatesForUser(userId)
                      .ToList()
                      .ForEach(u => times.AddRange(GetTimes(u.Id)));

            return times;
        }

        #endregion
    }
}