using FluentValidation.Results;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enum;
using GoodHamburger.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Application.Services
{
    public class SandwicheService : ISandwichService
    {
        private readonly AppDbContext _context;

        public SandwicheService(AppDbContext context)
        {
            _context = context;
        }

        public Task<ValidationResult> CreateAsync(Product obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Where(x => x.Type == EnumProductType.Sandwich).ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return _context.Products.Where(x => x.Type == EnumProductType.Sandwich && x.Id == id).FirstOrDefault();
        }

        public Task<ValidationResult> UpdateAsync(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}
