using AutoMapper;
using Eklm.Core.DomainModels;

namespace Eklm.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HFunc, HFuncResource>();
            CreateMap<HFuncResource, HFunc>();
        }

    }
}