using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadicalMotor.Models
{
	public class Appointment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string AppointmentId { get; set; }

		[Required]
		[ForeignKey("Account")]
		public string AccountId { get; set; }

		[Required]
		public DateTime DateCreated { get; set; }

		public virtual Account Account { get; set; }
	}
}
