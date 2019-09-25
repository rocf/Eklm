using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eklm.Core.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Eklm.Infrastructure.Repositories
{
    public class HFuncRepository : IHFuncRepository
    {
        private readonly EklmContext _eklmContext;
        private IHFuncRepository _ihFuncRepositoryImplementation;

        public HFuncRepository(EklmContext eklmContext)
        {
            _eklmContext = eklmContext;
        }

        public async Task<IEnumerable<HFunc>> GetHFuncsAsync()
        {
            return await _eklmContext.HFuncs.ToListAsync();
        }

        public async Task<HFunc> GetHFuncByIdAsync(string id)
        {
            //var hFunc = await _eklmContext.HFuncs.FindAsync(id);
            var hFunc = await _eklmContext.HFuncs.SingleOrDefaultAsync(x => x.Id == id);
            return hFunc;
        }
    }
}