namespace FitnessCenterManagement.Models
{
    // Updated MemberAttendanceViewModel without Id properties
    public class MemberAttendanceViewModel
    {
        public string MemberName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
        public string MembershipType { get; set; }

    }
}
