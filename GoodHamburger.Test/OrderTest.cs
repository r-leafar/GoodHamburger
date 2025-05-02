using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enum;

namespace GoodHamburger.Test
{
    public class OrderTest
    {
        [Fact]
        public void GetDiscountPercentageWith10Percent()
        {

            var order = new Order();
            var items = new List<OrderItem>
            {
                new OrderItem() { Product = new Product { Type = EnumProductType.Sandwich } },
                new OrderItem() { Product = new Product { Type = EnumProductType.ExtraFries } }
            };
            order.Items = items;
            var discount = order.GetDiscountPercentage();

            Assert.Equal(0.1m, discount);
        }

        [Fact]
        public void GetDiscountPercentageWith15Percent()
        {
            var order = new Order();
            var items = new List<OrderItem>
            {
                new OrderItem() { Product = new Product { Type = EnumProductType.Sandwich } },
                new OrderItem() { Product = new Product { Type = EnumProductType.ExtraDrink } }
            };
            order.Items = items;
            var discount = order.GetDiscountPercentage();

            Assert.Equal(0.15m, discount);
        }

        [Fact]
        public void GetDiscountPercentageWith20Percent()
        {
            var order = new Order();
            var items = new List<OrderItem>
            {
                new OrderItem() { Product = new Product { Type = EnumProductType.Sandwich } },
                new OrderItem() { Product = new Product { Type = EnumProductType.ExtraDrink } },
                new OrderItem() { Product = new Product { Type = EnumProductType.ExtraFries } }
            };
            order.Items = items;
            var discount = order.GetDiscountPercentage();

            Assert.Equal(0.2m, discount);
        }
    }
}