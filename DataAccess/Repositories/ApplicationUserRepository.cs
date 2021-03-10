using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.DataContext;
using Model.Entities;
using Model.Stores;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationUserStore _store;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserRepository(ChatDbContext context, ApplicationUserStore store, SignInManager<ApplicationUser> signInManager)
            : base(context)
        {
            _store = store;
            _signInManager = signInManager;
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            return await _store.CreateAsync(user, CancellationToken.None);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }
            return await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper());
        }

        public async Task<bool> SignInAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result.Succeeded;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
