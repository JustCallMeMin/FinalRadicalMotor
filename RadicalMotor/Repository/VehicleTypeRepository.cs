using RadicalMotor.Models;

namespace RadicalMotor.Repository
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public VehicleType GetVehicleType(string vehicleTypeId)
        {
            return _context.VehicleTypes.FirstOrDefault(vt => vt.VehicleTypeId == vehicleTypeId);
        }

        public IEnumerable<VehicleType> GetAllVehicleTypes()
        {
            return _context.VehicleTypes.ToList();
        }

        public VehicleType AddVehicleType(VehicleType vehicleType)
        {
            _context.VehicleTypes.Add(vehicleType);
            _context.SaveChanges();
            return vehicleType;
        }

        public VehicleType UpdateVehicleType(VehicleType vehicleType)
        {
            _context.VehicleTypes.Update(vehicleType);
            _context.SaveChanges();
            return vehicleType;
        }

        public void DeleteVehicleType(string vehicleTypeId)
        {
            var vehicleType = _context.VehicleTypes.Find(vehicleTypeId);
            if (vehicleType != null)
            {
                _context.VehicleTypes.Remove(vehicleType);
                _context.SaveChanges();
            }
        }
    }
}
