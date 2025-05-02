using FluentValidation;
using FluentValidation.Results;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Order> _validator;

        public OrderService(AppDbContext context, IValidator<Order> validator)
        {
            _context = context;
            _validator = validator;
        }
        
        public async Task<ValidationResult> CreateAsync(Order order)
        {

            await PopulateOrderItems(order);

            order.discount = order.GetDiscountPercentage();
            order.Total = order.Items.Sum(i => i.ItemTotal) * (1 - order.discount);

            var validationResult = _validator.Validate(order);
            
            if (validationResult.IsValid)
            {
            _context.Order.Add(order);
            await _context.SaveChangesAsync(); 
            }

            return validationResult;
        }

        public async Task<Order> GetAsync(int id)
        {
            var order = await _context.Order.Include(o => o.Items).
               ThenInclude(i => i.Product).
               Where(o => o.Id == id).
               FirstOrDefaultAsync();
             return order;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            // return await _context.Order.ToListAsync();
            return await _context.Order.Include(o => o.Items).
                ThenInclude(i => i.Product).ToListAsync();
        }
        public bool Exists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ValidationResult> UpdateAsync(Order order)
        {
            var orderFromDb = await _context.Order.Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            if (orderFromDb == null)
            {
            return new ValidationResult(new List<ValidationFailure>
                    {
                        new ValidationFailure("Order", "Order doesn´t exists.")
                    });
            }

            orderFromDb.Items.Clear(); // Clear existing items

            await PopulateOrderItems(order);

            orderFromDb.Items.AddRange(order.Items); // Add new items

            orderFromDb.discount = orderFromDb.GetDiscountPercentage();
            orderFromDb.Total = orderFromDb.Items.Sum(i => i.ItemTotal) * (1 - orderFromDb.discount);
      

            _context.Entry(orderFromDb).State = EntityState.Modified;

            var validationResult = _validator.Validate(orderFromDb);


            if (validationResult.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(order.Id))
                    {
                        return  new ValidationResult(new List<ValidationFailure>
                        {
                            new ValidationFailure("Order", "Problem to update, order.")
                        });
                    }
                }
            }

            return validationResult;
        }

        private async Task PopulateOrderItems(Order order)
        {
            foreach (var item in order.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    item.Product = product;
                    item.ItemTotal = item.Amount * product.Price;
                }
            }
        }
        
    }
}
