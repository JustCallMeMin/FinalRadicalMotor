using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RadicalMotor.Models;
using System.Security.Claims;

namespace RadicalMotor.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountRepository(ApplicationDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> GetByIdAsync(string id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> AddAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AccountType> GetDefaultAccountTypeAsync(string typeName)
        {
            return await _context.AccountTypes.FirstOrDefaultAsync(at => at.TypeName == typeName);
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<string> CreateTokenAsync(Account account, bool rememberMe)
        {
            if (rememberMe)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.Email),
                    // Add additional claims as needed
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1)
                };

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return null;
            }
            else
            {
                return "Remember me is not enabled";
            }
        }
    }
}
