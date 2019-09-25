using System;
using AutoMapper;
using Eklm.Core.DomainModels;
using Eklm.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eklm.Core.Interfaces;

namespace Eklm.API.Controllers
{
   // [ApiController]
    [Route("api/[Controller]")]
    public class HFuncsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHFuncRepository _hFuncRepository;
        private readonly IMapper _mapper;

        public HFuncsController(IUnitOfWork unitOfWork, IHFuncRepository hFuncRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _hFuncRepository = hFuncRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hFuncs = await _hFuncRepository.GetHFuncsAsync();
            var hFuncResources = _mapper.Map<List<HFuncResource>>(hFuncs);
            return Ok(hFuncResources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHFunc(string id)
        {
            var hFunc = await _hFuncRepository.GetHFuncByIdAsync(id);
            if (hFunc == null)
            {
                return NotFound();
            }
            var hFuncResource = _mapper.Map<HFuncResource>(hFunc);
            return Ok(hFuncResource);

        }
    }
}