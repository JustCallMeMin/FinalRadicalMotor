using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadicalMotor.Models
{
	public class InstallmentContract
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string? InstallmentContractId { get; set; }

		[ForeignKey("Employee")]
		public string EmployeeId { get; set; }

		[ForeignKey("Customer")]
		public string CustomerId { get; set; }

		[ForeignKey("Vehicle")]
		public string ChassisNumber { get; set; }

		[ForeignKey("InstallmentPlan")]
		public string InstallmentPlanId { get; set; }
		public DateTime? DateCreated { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal MonthlyInstallment { get; set; }

		public Employee Employee { get; set; }
		public Customer Customer { get; set; }
		public Vehicle Vehicle { get; set; }
		public InstallmentPlan InstallmentPlan { get; set; }
	}
}
