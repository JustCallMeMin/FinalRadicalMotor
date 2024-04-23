using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RadicalMotor.Models
{
	public class Supplier
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string SupplierId { get; set; }

		[Required]
		[MaxLength(255)]
		public string Name { get; set; }

		[MaxLength(500)]
		public string Address { get; set; }

		[Phone]
		[MaxLength(20)]
		public string PhoneNumber { get; set; }
	}
}