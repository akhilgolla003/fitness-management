using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessCenterManagement.Models
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }

        [Required]
        public DateTime Schedule { get; set; }

        [Required]
        public int TrainerID { get; set; }

        [Required]
        public int MaxCapacity { get; set; }

        [Required]
        public int RoomNumber { get; set; }
    }
}
