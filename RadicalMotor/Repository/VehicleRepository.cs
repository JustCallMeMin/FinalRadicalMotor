using Microsoft.EntityFrameworkCore;
using RadicalMotor.DTO;
using RadicalMotor.Models;

namespace RadicalMotor.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public VehicleDTO GetVehicle(string chassisNumber)
        {
            var vehicle = _context.Vehicles
                .Include(v => v.VehicleType)
                .Where(v => v.ChassisNumber == chassisNumber)
                .Select(v => new VehicleDTO
                {
                    ChassisNumber = v.ChassisNumber,
                    VehicleName = v.VehicleName,
                    EntryDate = v.EntryDate,
                    Version = v.Version,
                    Price = v.Price,
                    VehicleTypeId = v.VehicleType.VehicleTypeId,
                    ImageUrls = v.VehicleImages.Select(img => img.ImageUrl).ToList()
                })
                .FirstOrDefault();

            return vehicle;
        }

        public IEnumerable<VehicleDTO> GetAllVehicles()
        {
            return _context.Vehicles
                .Include(v => v.VehicleType) // Make sure VehicleType is included
                .Select(v => new VehicleDTO
                {
                    ChassisNumber = v.ChassisNumber,
                    VehicleName = v.VehicleName,
                    EntryDate = v.EntryDate,
                    Version = v.Version,
                    Price = v.Price,
                    VehicleTypeId = v.VehicleType != null ? v.VehicleType.VehicleTypeId : null,
                    VehicleType = v.VehicleType != null ? v.VehicleType.TypeName : "Unknown",
                    ImageUrls = v.VehicleImages.Select(img => img.ImageUrl).ToList()
                })
                .ToList();
        }

        public Vehicle AddVehicle(Vehicle vehicle, List<VehicleImage> images)
        {
            _context.Vehicles.Add(vehicle);
            _context.VehicleImages.AddRange(images);
            _context.SaveChanges();
            return vehicle;
        }

        public Vehicle UpdateVehicle(Vehicle vehicle, List<VehicleImage> images)
        {
            _context.Vehicles.Update(vehicle);
            var existingImages = _context.VehicleImages.Where(img => img.ChassisNumber == vehicle.ChassisNumber).ToList();
            _context.VehicleImages.RemoveRange(existingImages);
            _context.VehicleImages.AddRange(images);
            _context.SaveChanges();
            return vehicle;
        }

        public void DeleteVehicle(string chassisNumber)
        {
            var vehicle = _context.Vehicles.Find(chassisNumber);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }
    }
}
