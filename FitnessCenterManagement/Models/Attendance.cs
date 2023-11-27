using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessCenterManagement.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }

        [Required]
        public int MemberID { get; set; }

        [Required]
        public int ClassID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }
    }
}
