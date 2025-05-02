using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Infrastructure.Persistence.Contexts;
using GoodHamburger.Domain.Enum;
using GoodHamburger.Application.Interfaces;
using AutoMapper;
using GoodHamburger.Api.DTOs;

namespace GoodHamburger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SandwichesController : ControllerBase
    {
        private readonly ISandwichService _sandwichService;
        private readonly IMapper _mapper;

        public SandwichesController(ISandwichService sandwichService, IMapper mapper)
        {
            _sandwichService = sandwichService;
            _mapper = mapper;
        }

        // GET: api/Sandwiches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetSandwiches()
        {
            var list = await _sandwichService.GetAllAsync();
            var dto = _mapper.Map<List<ProductResponse>>(list);
            return dto;
        }

        // GET: api/Sandwiches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetSandwich(int id)
        {
            var sandwich = await _sandwichService.GetAsync(id);

            if (sandwich == null)
            {
                return NotFound();
            }
             var dto = _mapper.Map<ProductResponse>(sandwich);
            return dto;
        }
    }
}
