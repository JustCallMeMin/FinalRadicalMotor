using System;
using System.Collections.Generic; // For ICollection
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadicalMotor.Models
{
	public class Customer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string? CustomerId { get; set; }

		[Required]
		[MaxLength(100)]
		public string FullName { get; set; }

		[MaxLength(20)]
		public string IdentityCard { get; set; }

		public DateTime DateOfBirth { get; set; }

		[MaxLength(10)]
		public string Gender { get; set; }

		[MaxLength(255)]
		public string Address { get; set; }

		[Phone]
		public string PhoneNumber { get; set; }
		public ICollection<Account> Accounts { get; set; }
	}
}
