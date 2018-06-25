using LawInOrder.Timesheet.Web.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LawInOrder.Timesheet.Web.DAL
{
    public class TimesheetContext : DbContext
    {
        public TimesheetContext() : base("TimesheetContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Time> Times { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}