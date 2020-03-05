using SupermarketAPI.Domain.Models;
using SupermarketAPI.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
    }
}
