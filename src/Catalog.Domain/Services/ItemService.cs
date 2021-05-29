using Catalog.Domain.Interfaces;
using Catalog.Domain.Models;
using Catalog.Domain.Models.Validations;
using Catalog.Domain.Notifications;
using System.Threading.Tasks;

namespace Catalog.Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemRepository _itemRepository;
        private readonly INotificator _notificator;

        public ItemService(IItemRepository itemRepository, IUnitOfWork unitOfWork, INotificator notificator)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _notificator = notificator;
        }

        public async Task<bool> Add(Item item)
        {
            var itemValidation = new ItemValidation();
            var validationResult = itemValidation.Validate(item);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    _notificator.Handle(new Notification(error.ErrorMessage));

                return false;
            }

            _itemRepository.Add(item);

            return await _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _itemRepository?.Dispose();
        }
    }
}
