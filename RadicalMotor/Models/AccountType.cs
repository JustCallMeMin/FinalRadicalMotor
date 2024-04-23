using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadicalMotor.Models
{
	public class AccountType
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string AccountTypeId { get; set; }

		[Required]
		[MaxLength(100)]
		public string TypeName { get; set; }
	}
}
