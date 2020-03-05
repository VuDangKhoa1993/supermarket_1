using SupermarketAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.Services.Communication
{
    public class ProductResponse : BaseResponse
    {
        public Product Product { get; private set; }
        public ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            Product = product;
        }
        /// <summary>
        /// Create a success response
        /// </summary>
        /// <param name="product"></param>
        public ProductResponse(Product product) : this(true, string.Empty, product)
        {
        }
        /// <summary>
        /// Create a failed response
        /// </summary>
        /// <param name="message"></param>
        public ProductResponse(string message): this(false, message, null)
        {

        }
    }
}
