using Microsoft.AspNetCore.Identity;
using Model.Entities;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        public Task<bool> SignInAsync(string email, string password);
        public Task SignOutAsync();
        Task<ApplicationUser> GetByEmailAsync(string email);
    }
}
