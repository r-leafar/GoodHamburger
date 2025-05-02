using GoodHamburger.Domain.Enum;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal discount { get; set; } = 0.0m;
        public decimal Total { get; set; }

        public decimal GetDiscountPercentage()
        {
            if (Items.Count == 0)
                return 0;


            var productTypes = Items
               .Select(i => i.Product.Type)
               .ToHashSet();

            bool hasSandwich = productTypes.Contains(EnumProductType.Sandwich);
            bool hasFries = productTypes.Contains(EnumProductType.ExtraFries);
            bool hasDrink = productTypes.Contains(EnumProductType.ExtraDrink);

            return (hasSandwich, hasFries, hasDrink) switch
            {
                (true, true, true) => 0.2m,
                (true, false, true) => 0.15m,
                (true, true, false) => 0.1m,
                _ => 0m
            };
        }
    }
    public class OrderItem
    {

        public int Id { get; set; }
        public virtual Order Order { get; set; } = null!;
        public int ProductId { get; set; }
        public virtual IProduct Product { get; set; } = null!;
        public int Amount { get; set; }
        public decimal ItemTotal { get; set; }

    }
}
