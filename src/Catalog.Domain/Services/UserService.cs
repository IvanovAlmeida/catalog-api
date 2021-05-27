using System;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Models;

namespace Catalog.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<User> Add(User user)
        {
            var userNameExists = (await _userRepository.Find(u => u.Username.Equals(user.Username))).Any();
            if(userNameExists)
                throw new Exception("Nome de Usuário já cadastrado!");

            var emailExists = (await _userRepository.Find(u => u.Email.Equals(user.Email))).Any();
            if(emailExists)
                throw new Exception("Email já cadastrado!");

            _userRepository.Add(user);
            await _unitOfWork.Commit();

            return user;
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}