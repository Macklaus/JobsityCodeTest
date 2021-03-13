using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Exceptions;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IApplicationUserRepository _repository;

        public UserController(IApplicationUserRepository repository) 
        {
            _repository = repository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> CreateAsync(ApplicationUser user)
        {
            if (user == null)
                return BadRequest(user);

            var applicationUser = user.MapToEntityModel();

            if (!applicationUser.Validate())
                return BadRequest(user);
            try
            {
                await _repository.CreateAsync(applicationUser);
                return Ok(user);
            }
            catch (IdentifierAlreadyInUseException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<bool> SignInAsync(string email, string password)
        {
            return await _repository.SignInAsync(email, password);
        }

        [HttpPost("signout")]
        public async Task SignOutAsync()
        {
            await _repository.SignOutAsync();
        }
    }
}
