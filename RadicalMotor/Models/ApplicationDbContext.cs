using Microsoft.EntityFrameworkCore;

namespace RadicalMotor.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InstallmentContract> InstallmentContracts { get; set; }
        public DbSet<InstallmentNotification> InstallmentNotifications { get; set; }
        public DbSet<InstallmentPlan> InstallmentPlans { get; set; }
        public DbSet<InstallmentHistory> InstallmentHistorys { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionDetail> PromotionDetails { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<VehicleImage> VehicleImages { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppointmentDetail>()
            .HasKey(ad => new { ad.AppointmentId, ad.ServiceId });

            modelBuilder.Entity<PromotionDetail>()
            .HasKey(pd => new { pd.InstallmentContractId, pd.PromotionId });

            modelBuilder.Entity<InstallmentContract>()
            .HasOne(ih => ih.Employee)
            .WithMany()
            .HasForeignKey(ih => ih.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InstallmentContract>()
            .HasOne(ih => ih.Customer)
            .WithMany()
            .HasForeignKey(ih => ih.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InstallmentContract>()
            .HasOne(ih => ih.InstallmentPlan)
            .WithMany()
            .HasForeignKey(ih => ih.InstallmentPlanId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InstallmentContract>()
            .HasOne(ih => ih.Vehicle)
            .WithMany()
            .HasForeignKey(ih => ih.ChassisNumber)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InstallmentHistory>()
            .HasOne(ih => ih.InstallmentContract)
            .WithMany()
            .HasForeignKey(ih => ih.InstallmentContractId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InstallmentHistory>()
            .HasOne(ih => ih.Employee)
            .WithMany()
            .HasForeignKey(ih => ih.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
		}
    }
}
