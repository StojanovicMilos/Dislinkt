using Dislinkt.Data;
using Dislinkt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dislinkt.Controllers
{
    [AllowAnonymous] //TODO
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<ApplicationUser> Get() => _userRepository.GetAll();
    }
}
