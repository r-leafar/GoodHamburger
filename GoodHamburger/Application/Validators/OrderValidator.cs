using FluentValidation;
using GoodHamburger.Api.DTOs;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace GoodHamburger.Application.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {

        private readonly IProductService _productService;

        public OrderValidator(IProductService productService) 
        {
            _productService = productService;

            RuleFor(order => order.Items)
                .NotEmpty()
                .Must(items => items.Count > 0)
                .When(order => order.Items != null && order.Items.Count > 0)
                .WithMessage("Order must contain at least one item.");

            RuleFor(order => order.Items)
                .NotEmpty()
                .Must(CheckAllProductsAreValids)
                .When(order => order.Items != null && order.Items.Count > 0)
                .WithMessage("There is products thats not exists please verify");

            RuleFor(order => order.Items)
                .NotEmpty()
                .Must(Items => Items.Count(i => i.Product.Type == EnumProductType.Sandwich) <= 1 )
                .When(o => o.Items?.Count > 0 && o.Items.All( x => x.Product !=null))
                .WithMessage(" An order cannot contain more than one sandwich.");

            RuleFor(order => order.Items)
               .NotEmpty()
               .Must(Items => Items.Count(i => i.Product.Type == EnumProductType.ExtraDrink) <= 1)
               .When(o => o.Items?.Count > 0 && o.Items.All(x => x.Product != null))
               .WithMessage(" An order cannot contain more than one drink.");

            RuleFor(order => order.Items)
               .NotEmpty()
               .Must(Items => Items.Count(i => i.Product.Type == EnumProductType.ExtraFries) <= 1)
               .When(o => o.Items?.Count > 0 && o.Items.All(x => x.Product != null))
               .WithMessage(" An order cannot contain more than one fries.");

            RuleFor(order => order.Items)
               .NotEmpty()
               .Must(items => items.All(x => x.Amount > 0))
               .When(order => order.Items != null && order.Items.Count > 0)
               .WithMessage("The order item must have quantity greather than zero");
        }
        private bool CheckAllProductsAreValids(List<OrderItem> items)
        {

            return items.All(item => _productService.Exists(item.ProductId));
        }

    }
}
