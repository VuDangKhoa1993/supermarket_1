using Microsoft.EntityFrameworkCore;
using SupermarketAPI.Domain.Models;
using SupermarketAPI.Domain.Repositories;
using SupermarketAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Product.Include(x => x.Category).ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Product.AddAsync(product);
        }

        public async Task<Product> FindProductById(int id)
        {
            return await _context.Product.FindAsync(id);
        }

        public void Update(Product product)
        {
            _context.Product.Update(product);
        }

        public void Delete(Product product)
        {
            _context.Product.Remove(product);
        }
    }
}
