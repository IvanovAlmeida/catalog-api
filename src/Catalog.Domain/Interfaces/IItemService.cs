using Catalog.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Catalog.Domain.Interfaces
{
    public interface IItemService : IDisposable
    {
        Task<bool> Add(Item item);
        Task<bool> Update(Item item);
        Task<bool> Disable(int id);
        Task<bool> Reactivate(int id);
    }
}
