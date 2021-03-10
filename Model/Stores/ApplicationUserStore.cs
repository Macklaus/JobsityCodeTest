using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Model.DataContext;
using Model.Entities;
using Model.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Model.Stores
{
    public class ApplicationUserStore : IUserPasswordStore<ApplicationUser>
    {
        private readonly ChatDbContext _context;
        public ApplicationUserStore(ChatDbContext context)
        {
            _context = context;
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (!user.Validate())
            {
                return IdentityResult.Failed(new IdentityError { Description = Constants.NotValidUser });
            }
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                if (await _context.ApplicationUsers.AnyAsync(u => u.Email == user.Email, cancellationToken))
                {
                    return IdentityResult.Failed(new IdentityError { Description =  Constants.EmailAlreadyUsed});
                }

                var newUser = new ApplicationUser();
                newUser.Update(user);
                await _context.ApplicationUsers.AddAsync(newUser, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();

                return IdentityResult.Success;
            }
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(IdentityResult.Success);
        }

        public void Dispose() { }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (int.TryParse(userId, out var id) == false)
            {
                return new ApplicationUser();
            }
            return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToUpper() == userName.ToUpper(), cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.NormalizedUserName);
        }

        public async Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.PasswordHash);
        }

        public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Id.ToString());
        }

        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserName);
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }

        public async Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            // do nothing
            await Task.Run(() => { });
        }

        public async Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            await Task.Run(() => { });
        }
        public async Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            await Task.Run(() => { });
        }
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (!user.Validate())
            {
                return IdentityResult.Failed(new IdentityError { Description = Constants.NotValidUser });
            }

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                ApplicationUser target = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);
                
                if (target == null)
                    return IdentityResult.Failed(new IdentityError { Description = Constants.NotUserFound });

                target.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();
                return IdentityResult.Success;
            }
        }
    }
}
