namespace GoodHamburger.Api.DTOs
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();

        public decimal discount { get; set; } = 0.0m;
        public decimal Total { get; set; }
    }

    public class OrderItemResponse
    {

        public int ProductId { get; set; }

        public int Amount { get; set; }
        public decimal ItemTotal { get; set; }
    }
}
