using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadicalMotor.Models
{
	public class PromotionDetail
	{
		public string InstallmentContractId { get; set; }

		public string PromotionId { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal ActualDiscount { get; set; }

		public InstallmentContract InstallmentContract { get; set; }
		public Promotion Promotion { get; set; }
	}
}
