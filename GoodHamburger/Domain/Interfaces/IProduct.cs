using GoodHamburger.Domain.Enum;
using System.Text.Json.Serialization;

namespace GoodHamburger.Domain.Interfaces
{
    public abstract class IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EnumProductType Type { get; set; }
        public decimal Price { get; set; }
    }
}
