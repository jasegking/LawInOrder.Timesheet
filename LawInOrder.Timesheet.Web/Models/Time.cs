using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LawInOrder.Timesheet.Web.Models
{
    public class Time
    {
        public int Id { get; set; }

        [DisplayName("User")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} is required")]
        public int UserId { get; set; }

        [DisplayName("User")]
        public virtual User User { get; set; }

        [DisplayName("Date worked")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime? Date { get; set; }

        [DisplayName("Hours worked")]
        [Required]
        [Range(1, 24, ErrorMessage = "{0} is required")]
        public int? HoursWorked { get; set; }
    }
}