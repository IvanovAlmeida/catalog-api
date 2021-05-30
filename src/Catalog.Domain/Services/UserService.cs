using System;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Models;
using Catalog.Domain.Models.Validations;
using Catalog.Domain.Notifications;
using Microsoft.AspNetCore.Identity;

namespace Catalog.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly INotificator _notificator;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, INotificator notificator)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _notificator = notificator;
        }

        public async Task<bool> Add(User user)
        {
            var userValidation = new UserValidation(_userRepository);

            var validationResult = await userValidation.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                foreach(var error in validationResult.Errors)
                {
                    _notificator.Handle(new Notification(error.ErrorMessage));
                }

                return false;
            }

            var passwordHashser = new PasswordHasher<User>();
            user.Password = passwordHashser.HashPassword(user, user.Password);

            _userRepository.Add(user);
            return await _unitOfWork.Commit();
        }
        
        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}