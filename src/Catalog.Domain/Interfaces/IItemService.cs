using Catalog.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Catalog.Domain.Interfaces
{
    public interface IItemService : IDisposable
    {
        Task<bool> Add(Item item);
    }
}
