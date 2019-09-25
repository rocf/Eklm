using System.Threading.Tasks;
using Eklm.Core.Interfaces;

namespace Eklm.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EklmContext _eklmContext;

        public UnitOfWork(EklmContext eklmContext)
        {
            _eklmContext = eklmContext;
        }
        public async Task<bool> SaveAsync()
        {
            return await _eklmContext.SaveChangesAsync() > 0;
        }
    }
}