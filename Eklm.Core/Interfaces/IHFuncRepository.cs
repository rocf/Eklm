using System.Collections.Generic;
using System.Threading.Tasks;
using Eklm.Core.DomainModels;

namespace Eklm.Infrastructure.Repositories
{
    public interface IHFuncRepository
    {
        Task<IEnumerable<HFunc>> GetHFuncsAsync();
        Task<HFunc> GetHFuncByIdAsync(string id);
    }
}