using System.ComponentModel;

namespace LawInOrder.Timesheet.Web.Models
{
    public class User
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string DisplayName { get; set; }

        [DisplayName("Login")]
        public string Login { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Email address")]
        public string Email { get; set; }

        [DisplayName("Manager")]
        public int? ManagerId { get; set; }

        [DisplayName("Manager")]
        public virtual User Manager { get; set; }
    }
}