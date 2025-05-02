using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Infrastructure.Persistence.Contexts;
using GoodHamburger.Api.DTOs;
using AutoMapper;
using GoodHamburger.Application.Interfaces;
using FluentValidation;

namespace GoodHamburger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrder()
        {
            var orders = await _orderService.GetAllAsync();
            var dto = _mapper.Map<List<OrderResponse>>(orders);

            return dto;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(int id)
        {
            var order = await _orderService.GetAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<OrderResponse>(order);

            return dto;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderUpdateRequest orderRequest)
        {
            if (id != orderRequest.OrderId)
            {
                var problemDetails = new HttpValidationProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation failed",
                    Detail = "The ID in the URL does not match the ID in the JSON body",
                    Instance = GetPathAndQuery()
                };
                return BadRequest(problemDetails);
            }

            var   order =  _mapper.Map<Order>(orderRequest);

            var validationResult =  await _orderService.UpdateAsync(order);

            if (!validationResult.IsValid)
            {
                var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation failed",
                    Detail = "One or more validation erros occurred.",
                    Instance = GetPathAndQuery()
                };

                return BadRequest(problemDetails);
            }
            
            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> PostOrder(OrderRequest createdOrder, IValidator<OrderRequest> validatorRequest,IValidator<Order> validator)
        {
            var validationResult = await validatorRequest.ValidateAsync(createdOrder);

            if (!validationResult.IsValid)
            {
                var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation failed",
                    Detail = "One or more validation erros occurred.",
                    Instance = GetPathAndQuery()
                };

                return BadRequest(problemDetails);
            }

            var order = _mapper.Map<Order>(createdOrder);

             validationResult =  await _orderService.CreateAsync(order);

            if (!validationResult.IsValid)
            {
                var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation failed",
                    Detail = "One or more validation erros occurred.",
                    Instance = GetPathAndQuery()
                };

                return BadRequest(problemDetails);
            }

            return CreatedAtAction("GetOrder", new { id = order.Id }, createdOrder);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteAsync(id);

            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        private string GetPathAndQuery()
        {
            var request = HttpContext.Request;
            return request.Path + request.QueryString;
        }

    }
}
