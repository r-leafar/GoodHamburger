using AutoMapper.Configuration;
using FluentValidation;
using FluentValidation.Results;
using GoodHamburger.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.Application.Validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            /*
            RuleForEach(order => order.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId)
                    .GreaterThan(0)
                    .WithMessage("The product identifier must be greater than zero.");

                item.RuleFor(i => i.Quantity)
                    .Must(items => items > 0)
                    .WithMessage("The quantity must be greater than zero.");


            });*/
            
            RuleFor(order => order.Items)
                .NotEmpty()
                .Must(items => items.Count > 0)
                .When(order => order.Items != null && order.Items.Count > 0)
                .WithMessage("Order must contain at least one item.");

            RuleFor(order => order.Items)
            .NotEmpty()
            .Must(items => items.All( x => x.ProductId > 0))
            .When(order => order.Items != null && order.Items.Count > 0)
            .WithMessage("The code of order item must be greater than 0");

            RuleFor(order => order.Items)
               .NotEmpty()
               .Must(items => items.All( x => x.Quantity > 0))
               .When(order => order.Items != null && order.Items.Count > 0)
               .WithMessage("The order item must have quantity greather than zero");
        }

        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext, ValidationResult result)
        {

            return result;
        }
    }
}
