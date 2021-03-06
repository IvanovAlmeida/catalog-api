using System;
using System.Threading.Tasks;
using Catalog.Domain.Models;

namespace Catalog.Domain.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<bool> Add(User user);
    }
}