using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadicalMotor.Models
{
	public class InstallmentNotification
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string NotificationId { get; set; }

		[ForeignKey("Account")]
		public string AccountId { get; set; }

		[ForeignKey("InstallmentContract")]
		public string InstallmentContractId { get; set; }

		[Required]
		[MaxLength(50)]
		public string NotificationType { get; set; }

		[Required]
		[MaxLength(500)]
		public string NotificationContent { get; set; }

		public DateTime DateSent { get; set; }

		[MaxLength(20)]
		public string Status { get; set; }

		public Account Account { get; set; }
		public InstallmentContract InstallmentContract { get; set; }
	}
}
