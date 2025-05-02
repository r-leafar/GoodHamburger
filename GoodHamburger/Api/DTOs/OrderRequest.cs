using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GoodHamburger.Api.DTOs
{
    public class OrderRequest
    {
        public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();

    }

    public class OrderUpdateRequest
    {
        public int OrderId { get; set; }
        public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();

    }

    public class OrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

   
}
