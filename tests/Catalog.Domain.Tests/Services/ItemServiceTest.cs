using Catalog.Domain.Interfaces;
using Catalog.Domain.Models;
using Catalog.Domain.Models.Validations;
using Catalog.Domain.Notifications;
using Catalog.Domain.Services;
using Moq;
using Moq.AutoMock;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Domain.Tests.Services
{
    public class ItemServiceTest
    {
        private readonly AutoMocker _mocker;
        private readonly ItemService _itemService;
        private readonly INotificator _notificator;

        public ItemServiceTest()
        {
            _notificator = new Notificator();

            _mocker = new AutoMocker();
            _mocker.Use(_notificator);

            _itemService = _mocker.CreateInstance<ItemService>();
        }

        [Fact(DisplayName = "Item deve inserir com sucesso")]
        [Trait("Categoria", "Catalog")]
        public async Task Item_ItemValido_DeveInserirComSucesso()
        {
            // Arrange
            var item = new Item
            { 
                Name = "Produto 1",
                ItemType = ItemType.Product,
                Value = 25.00m
            };

            _mocker.GetMock<IItemRepository>().Setup(r => r.Add(It.IsAny<Item>()));
            _mocker.GetMock<IUnitOfWork>().Setup(r => r.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _itemService.Add(item);

            // Assert
            Assert.True(result);
            Assert.False(_notificator.HasNotification());
            _mocker.GetMock<IItemRepository>().Verify(m => m.Add(It.IsAny<Item>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(m => m.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Item deve retornar erro ao inserir")]
        [Trait("Categoria", "Catalog")]
        public async Task Item_ItemInvalido_DeveRetornarErroAoInserir()
        {
            // Arrange
            var item = new Item
            {
                Name = "",
                Value = 0m
            };

            // Act
            var result = await _itemService.Add(item);

            // Assert
            Assert.False(result);
            Assert.True(_notificator.HasNotification());
            Assert.Equal(3, _notificator.GetNotifications().Count);

            Assert.Contains(ItemValidation.ErroMsgEmptyName, _notificator.GetNotifications().Select(m => m.Message));
            Assert.Contains(ItemValidation.ErroMsgItemTypeInvalid, _notificator.GetNotifications().Select(m => m.Message));
            Assert.Contains(ItemValidation.ErroMsgValueGreaterThan, _notificator.GetNotifications().Select(m => m.Message));

            _mocker.GetMock<IItemRepository>().Verify(m => m.Add(It.IsAny<Item>()), Times.Never);
            _mocker.GetMock<IUnitOfWork>().Verify(m => m.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Item deve desativar com sucesso")]
        [Trait("Categoria", "Catalog")]
        public async Task Item_Desativar_DeveDesativarComSucesso()
        {
            // Arrange
            var itemId = 3;
            _mocker.GetMock<IUnitOfWork>().Setup(r => r.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _itemService.Disable(itemId);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IItemRepository>().Verify(r => r.Disable(It.IsAny<int>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(r => r.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Item deve reativar com sucesso")]
        [Trait("Categoria", "Catalog")]
        public async Task Item_Reativar_DeveReativarComSucesso()
        {
            // Arrange
            var itemId = 3;
            _mocker.GetMock<IUnitOfWork>().Setup(r => r.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _itemService.Reactivate(itemId);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IItemRepository>().Verify(r => r.Reactivate(It.IsAny<int>()), Times.Once);
            _mocker.GetMock<IUnitOfWork>().Verify(r => r.Commit(), Times.Once);
        }
    }
}
