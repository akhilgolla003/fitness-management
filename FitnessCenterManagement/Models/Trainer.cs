using System.ComponentModel.DataAnnotations;

namespace FitnessCenterManagement.Models
{
    public class Trainer
    {
        [Key]
        public int TrainerID { get; set; }

        [Required]
        [StringLength(50)]
        public string TrainerName { get; set; }

        [Required]
        [StringLength(50)]
        public string Specialization { get; set; }

        [Required]
        [StringLength(50)]
        public string Certification { get; set; }

        [Required]
        public int ExperienceYears { get; set; }
    }
}
