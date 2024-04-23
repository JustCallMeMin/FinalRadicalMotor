using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadicalMotor.Models
{
    public class VehicleImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [ForeignKey("Vehicle")]
        public string ChassisNumber { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
