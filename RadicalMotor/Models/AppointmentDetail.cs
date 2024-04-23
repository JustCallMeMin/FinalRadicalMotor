using System.ComponentModel.DataAnnotations.Schema;

namespace RadicalMotor.Models
{
	public class AppointmentDetail
	{
		[ForeignKey("Appointment")]
		public string AppointmentId { get; set; }

		[ForeignKey("Service")]
		public string ServiceId { get; set; }

		public DateTime ServiceDate { get; set; } 
		public string Notes { get; set; }

		public virtual Appointment Appointment { get; set; }
		public virtual Service Service { get; set; }
	}

}
