using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadicalMotor.Models
{
	public class InstallmentHistory
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		public string InstallmentHistoryId { get; set; }

		public string InstallmentContractId { get; set; }

		public DateTime TransactionDate { get; set; }

		public decimal AmountPaid { get; set; }

		public string PaymentMethod { get; set; }

		public DateTime NextPaymentDate { get; set; }

		[MaxLength(50)]
		public string TransactionStatus { get; set; }

		[ForeignKey("EmployeeId")]
		public string EmployeeId { get; set; }

		[ForeignKey("AccountId")]
		public string AccountId { get; set; }

		public InstallmentContract InstallmentContract { get; set; }
		public Employee Employee { get; set; }
		public Account Account { get; set; }
	}
}
