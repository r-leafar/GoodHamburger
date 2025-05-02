using AutoMapper;
using GoodHamburger.Api.DTOs;
using GoodHamburger.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtrasController : ControllerBase
    {
        private readonly IExtraService _extraService;
        private readonly IMapper _mapper;

        public ExtrasController(IExtraService extraService, IMapper mapper)
        {
            _extraService = extraService;
            _mapper = mapper;
        }

        // GET: api/Extras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetExtras()
        {
            var list = await _extraService.GetAllAsync();
            var dto = _mapper.Map<List<ProductResponse>>(list);
            return dto;
        }

        // GET: api/Extras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetExtra(int id)
        {
            var extra =  await _extraService.GetAsync(id);
            var dto = _mapper.Map<ProductResponse>(extra);
            return dto;
        }

    }
}
