using RadicalMotor.Models;

namespace RadicalMotor.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetOrCreateCustomerAsync(string fullName, string phoneNumber);
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByPhoneNumberAsync(string phoneNumber);
        Task<Customer> GetByIdAsync(string id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(string id);
    }
}
