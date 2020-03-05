using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupermarketAPI.Domain.Models;
using SupermarketAPI.Domain.Services;
using SupermarketAPI.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;
using SupermarketAPI.Extensions;

namespace SupermarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResource>> ListAsync()
        {
            var result = await _productService.ListAsync();
            var productResource = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(result);
            return productResource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.SaveAsync(product);
            if(!result.Success)
            {
                return BadRequest(result.Message);
            }
            var productResource = _mapper.Map<Product, ProductResource>(result.product);
            return Ok(productResource);
        }
    }
}