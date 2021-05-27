using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.V1
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("")]
        public async Task<IEnumerable<User>> Search()
        {
            return await _userRepository.GetAll();
        }
    }
}