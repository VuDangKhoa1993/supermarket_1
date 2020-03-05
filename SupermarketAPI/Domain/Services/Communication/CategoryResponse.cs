using SupermarketAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Domain.Services.Communication
{
    public class CategoryResponse : BaseResponse
    {
        public Category Category { get; private set; }
        private CategoryResponse(bool success, string message, Category category): base(success, message)
        {
            Category = category;
        }
        /// <summary>
        /// Create a success response
        /// </summary>
        /// <param name="category">Saved category</param>
        public CategoryResponse(Category category) : this(true, string.Empty, category) { }

        /// <summary>
        /// Create a failed response
        /// </summary>
        /// <param name="message"></param>
        public CategoryResponse(string message) : this(false, message, null) { }
    }
}
