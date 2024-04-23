using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RadicalMotor.Models
{
	public class Vehicle
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public string ChassisNumber { get; set; } 

		[Required]
		[MaxLength(255)]
		public string VehicleName { get; set; } 

		public DateTime EntryDate { get; set; } 

		[MaxLength(100)]
		public string Version { get; set; }
		public string Price { get; set; }

		[ForeignKey("VehicleType")]
		public string VehicleTypeId { get; set; } 

		public VehicleType VehicleType { get; set; }

        public ICollection<VehicleImage> VehicleImages { get; set; }
    }
}
