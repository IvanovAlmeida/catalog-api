using Catalog.Domain.Interfaces;
using Catalog.Domain.Models;
using Catalog.Domain.Models.Validations;
using Moq.AutoMock;
using System.Linq;
using Xunit;

namespace Catalog.Domain.Tests.Validations
{
    public class UserValidationTest
    {
        private AutoMocker _mocker;
        private readonly IUserRepository _userRepository;

        public UserValidationTest()
        {
            _mocker = new AutoMocker();

            _userRepository = _mocker.Get<IUserRepository>();
        }

        [Fact(DisplayName = "User valid must return true")]
        [Trait("Categoria", "Validation - User")]
        public void UserValidation_UserValid_MustReturnTrue()
        {
            // Arrange
            var user = new User
            {
                Name = "User 134",
                Email = "user123@email.com",
                Password = "@Teste123"
            };

            var userValidation = new UserValidation(_userRepository);

            // Act
            var result = userValidation.Validate(user);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "User invalid must return false")]
        [Trait("Categoria", "Validation - User")]
        public void UserValidation_UserInvalid_MustReturnFalse()
        {
            // Arrange
            var user = new User
            {
                Name = "",
                Email = "",
                Password = ""
            };

            var userValidation = new UserValidation(_userRepository);

            // Act
            var result = userValidation.Validate(user);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "User with password invalid must return false")]
        [Trait("Categoria", "Validation - User")]
        public void UserValidation_UserWithPasswordInvalid_MustReturnFalse()
        {
            // Arrange
            var user = new User
            {
                Name = "User 1",
                Email = "user1@email.com",
                Password = ""
            };

            var userValidation = new UserValidation(_userRepository);

            // Act
            var result = userValidation.Validate(user);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(UserValidation.MsgErrorEmptyPassword, result.Errors.Select(e => e. ErrorMessage));
            Assert.Contains(UserValidation.MsgErrorMinLenPassword, result.Errors.Select(e => e. ErrorMessage));
            Assert.Contains(UserValidation.MsgErrorPasswordDoesNotContainLowerChars, result.Errors.Select(e => e. ErrorMessage));
            Assert.Contains(UserValidation.MsgErrorPasswordDoesNotContainUpperChars, result.Errors.Select(e => e. ErrorMessage));
            Assert.Contains(UserValidation.MsgErrorPasswordHasNoNumbers, result.Errors.Select(e => e. ErrorMessage));
            Assert.Contains(UserValidation.MsgErrorPasswordHasNoSymbols, result.Errors.Select(e => e. ErrorMessage));
        }
    }
}
