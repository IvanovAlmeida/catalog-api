using System.Threading.Tasks;

namespace Catalog.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}