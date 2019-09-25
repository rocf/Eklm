using System.Threading.Tasks;

namespace Eklm.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveAsync();
    }
}