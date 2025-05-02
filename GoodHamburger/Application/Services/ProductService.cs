using GoodHamburger.Application.Interfaces;
using GoodHamburger.Infrastructure.Persistence.Contexts;

namespace GoodHamburger.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public bool Exists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
