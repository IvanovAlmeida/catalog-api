using Catalog.Domain.Interfaces;
using Catalog.Domain.Notifications;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Domain.Tests
{
    public class NotificatorTest
    {
        private readonly AutoMocker _mocker;
        private readonly Mock<Notificator> _notificator;

        public NotificatorTest()
        {
            _mocker = new AutoMocker();
            _notificator = new Mock<Notificator>();
        }

        [Fact(DisplayName = "Adicionar Notificação")]
        [Trait("Categoria", "Notificator")]
        public void Notificator_Adicionar_DeveAdicionar()
        {
            // Arrange
            var notification = new Notification("Erro 1");

            // Act
            _notificator.Object.Handle(notification);

            // Assert
            Assert.Single(_notificator.Object.GetNotifications());
            Assert.True(_notificator.Object.HasNotification());
        }
    }
}
