using LawInOrder.Timesheet.Web.DAL;
using LawInOrder.Timesheet.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace LawInOrder.Timesheet.Web.Controllers
{
    public class HomeController : Controller
    {
        private TimesheetContext db = new TimesheetContext();

        /// <summary>
        /// Default view for the application displaying a report of time entered for self and subordinates
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            var times = db.Times.Where(t => t.UserId == 1)
                                .Include(t => t.User)
                                .OrderBy(t => t.Date);
            return View(times.ToList());
        }

        /// <summary>
        /// Set up the page to allow the user to add a new timesheet entry
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddTime()
        {
            Time time = new Time() { UserId = 1 };
            return View(time);
        }

        /// <summary>
        /// Save the time entry for the current user
        /// </summary>
        /// <param name="time">Time object to save to the database</param>
        /// <returns></returns>
        [Authorize]
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
                    Session["LoggedInUserId"] = user.Id;
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}