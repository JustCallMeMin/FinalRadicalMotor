using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RadicalMotor.Models
{
	public class Employee
	{
		[Key] 
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string EmployeeId { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		public DateTime DateOfBirth { get; set; }

		[MaxLength(50)]
		public string Position { get; set; }

		[MaxLength(255)]
		public string Address { get; set; }
	}
}