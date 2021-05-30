using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.ViewModels;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.V1
{
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IUserService userService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userService = userService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("")]
        public async Task<IEnumerable<UserViewModel>> Search()
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetAll());
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] UserAddViewModel userModel)
        {
            try
            {
                var user = _mapper.Map<User>(userModel);
                await _userService.Add(user);                

                return Ok(new {
                    status = true,
                    data = _mapper.Map<UserViewModel>(user)
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new {
                    status = false,
                    errors = new[] {
                        ex.Message
                    } 
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Disable(int id)
        {
            _userRepository.Disable(id);
            await _unitOfWork.Commit();
            return Ok(new {
                status = true
            });
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Reactivate(int id)
        {
            _userRepository.Reactivate(id);
            await _unitOfWork.Commit();
            return Ok(new {
                status = true
            });
        }
    }
}