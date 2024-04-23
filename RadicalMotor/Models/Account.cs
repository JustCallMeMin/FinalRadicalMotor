using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadicalMotor.Models
{
	public class Account
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string AccountId { get; set; }

		//[Required]
		[MaxLength(50)]
		public string Username { get; set; }

		//[Required]
		[MaxLength(256)]
		public string Password { get; set; }

		//[Required]
		[EmailAddress]
		public string Email { get; set; }

		public DateTime AccountCreationDate { get; set; }
		[Phone]
		public string? PhoneNumber { get; set; }

		[ForeignKey("Customer")]
		public string? CustomerId { get; set; }

		[ForeignKey("AccountType")]
		public string AccountTypeId { get; set; }

		[ForeignKey("InstallmentContract")]
		public string? InstallmentContractId { get; set; }
		public Customer Customer { get; set; }
		public ICollection<InstallmentContract> InstallmentContracts { get; set; }
		public AccountType AccountType { get; set; }

	}
}
