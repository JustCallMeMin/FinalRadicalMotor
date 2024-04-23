namespace RadicalMotorAPI.DTO
{
	public class AppointmentDTO
	{
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public string ServiceId { get; set; }
		public string ServiceDate { get; set; }
		public DateTime GetServiceDateAsDateTime()
		{
			DateTime parsedDate;
			if (DateTime.TryParse(ServiceDate, out parsedDate))
			{
				return parsedDate;
			}
			throw new ArgumentException("Invalid date format", nameof(ServiceDate));
		}
		public string Notes { get; set; }
	}

}
