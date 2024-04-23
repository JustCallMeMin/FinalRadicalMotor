using Microsoft.EntityFrameworkCore;
using RadicalMotor.Models;
using RadicalMotor.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Customer> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
    }

    public async Task<Customer> GetOrCreateCustomerAsync(string fullName, string phoneNumber)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);

        if (customer == null)
        {
            customer = new Customer
            {
                FullName = fullName,
                PhoneNumber = phoneNumber
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        return customer;
    }

    public async Task<Customer> GetByIdAsync(string id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task AddAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
