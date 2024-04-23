using Microsoft.EntityFrameworkCore;
using RadicalMotor.Models;
using RadicalMotor.Repository;

public class ServiceRepository : IServiceRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Service> GetServiceByIdAsync(string serviceId)
    {
        return await _context.Services
                             .FirstOrDefaultAsync(s => s.ServiceId == serviceId);
    }

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<Service> AddServiceAsync(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task UpdateServiceAsync(Service service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteServiceAsync(string serviceId)
    {
        var service = await _context.Services
                                   .FirstOrDefaultAsync(s => s.ServiceId == serviceId);
        if (service != null)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
}
