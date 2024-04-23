using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RadicalMotor.Models
{
	public class VehicleType
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public string VehicleTypeId { get; set; } 

		[Required]
		[MaxLength(255)]
		public string TypeName { get; set; }

		[ForeignKey("Supplier")]
		public string SupplierId { get; set; } 

		public Supplier Supplier { get; set; }
	}
}