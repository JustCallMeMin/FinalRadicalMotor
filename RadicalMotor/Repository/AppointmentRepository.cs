using Microsoft.EntityFrameworkCore;
using RadicalMotor.Models;

namespace RadicalMotor.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AppointmentDetail> GetAppointmentDetailByIdAsync(string appointmentId, string serviceId)
        {
            return await _context.AppointmentDetails
                .FirstOrDefaultAsync(ad => ad.AppointmentId == appointmentId && ad.ServiceId == serviceId);
        }

        public async Task<IEnumerable<AppointmentDetail>> GetAppointmentDetailsByAppointmentIdAsync(string appointmentId)
        {
            return await _context.AppointmentDetails
                .Where(ad => ad.AppointmentId == appointmentId)
                .ToListAsync();
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task AddAppointmentDetailAsync(AppointmentDetail appointmentDetail)
        {
            _context.AppointmentDetails.Add(appointmentDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentDetailAsync(AppointmentDetail appointmentDetail)
        {
            _context.AppointmentDetails.Update(appointmentDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentDetailAsync(string appointmentId, string serviceId)
        {
            var appointmentDetail = await GetAppointmentDetailByIdAsync(appointmentId, serviceId);
            if (appointmentDetail != null)
            {
                _context.AppointmentDetails.Remove(appointmentDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
