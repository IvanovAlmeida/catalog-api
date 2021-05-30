using Moq;
using Xunit;
using Moq.AutoMock;
using System.Threading.Tasks;

using Catalog.Domain.Models;
using Catalog.Domain.Services;
using Catalog.Domain.Interfaces;
using Catalog.Domain.Notifications;

namespace Catalog.Domain.Tests.Services
{
    public class UserServiceTest
    {
        private readonly AutoMocker _mocker;
        private readonly UserService _userService;
        private readonly INotificator _notificator;

        public UserServiceTest()
        {
            _notificator = new Notificator();
            _mocker = new AutoMocker();

            _mocker.Use(_notificator);
            _userService = _mocker.CreateInstance<UserService>();
        }

        [Fact(DisplayName = "User must successfully add")]
        [Trait("Categoria", "Service - User")]
        public async Task User_UserValid_MustSuccessfullyAdd()
        {
            // Arrange
            var user = new User 
            { 
                Name = "Usuário 1",
                Email = "email@email.com",
                Password = "@Teste123"
            };

            _mocker.GetMock<IUnitOfWork>().Setup(m => m.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _userService.Add(user);

            // Assert
            Assert.True(result);
            Assert.False(_notificator.HasNotification());
            _mocker.GetMock<IUserRepository>().Verify(r => r.Add(It.IsAny<User>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(r => r.Commit(), Times.Once);
        }

        [Fact(DisplayName = "User add must return error")]
        [Trait("Categoria", "Service - User")]
        public async Task User_UserInvalid_MustReturnError()
        {
            // Arrange
            var user = new User
            {
                Name = "",
                Email = "",
                Password = ""
            };
            _mocker.GetMock<IUnitOfWork>().Setup(m => m.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _userService.Add(user);

            // Assert
            Assert.False(result);
            Assert.True(_notificator.HasNotification());
            _mocker.GetMock<IUserRepository>().Verify(r => r.Add(It.IsAny<User>()), Times.Never);
            _mocker.GetMock<IUnitOfWork>().Verify(r => r.Commit(), Times.Never);
        }
    }
}   
