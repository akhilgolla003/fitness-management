using System.ComponentModel.DataAnnotations;

namespace FitnessCenterManagement.Models
{
    public class Member
    {
        [Key]
        public int MemberID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string MembershipType { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
