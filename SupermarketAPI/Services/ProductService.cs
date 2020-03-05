﻿using SupermarketAPI.Domain.Models;
using SupermarketAPI.Domain.Repositories;
using SupermarketAPI.Domain.Services;
using SupermarketAPI.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            try
            {
                var existingProduct = await _productRepository.FindProductById(id);
                if(existingProduct == null)
                {
                    return new ProductResponse("Not Found!");
                }
                _productRepository.Delete(existingProduct);
                await _unitOfWork.CompleteTask();
                return new ProductResponse(existingProduct);
            }
             catch(Exception ex)
            {
                return new ProductResponse(ex.Message);
            }
        }

        public async Task<ProductResponse> GetDetailAsync(int id)
        {
            try
            {
                var existingProduct = await _productRepository.FindProductById(id);
                if(existingProduct == null)
                {
                    return new ProductResponse("Product doesn't exist");
                }
                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
                if (existingCategory == null)
                {
                    return new ProductResponse("Invalid Category");
                }
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteTask();
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse(ex.Message);
            }
        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.FindProductById(id);
            if (existingProduct == null)
            {
                return new ProductResponse("Invalid Product");
            }
            existingProduct.Name = product.Name;
            existingProduct.QuantityInPackage = product.QuantityInPackage;
            existingProduct.UnitOfMeasurement = product.UnitOfMeasurement;
            existingProduct.CategoryId = product.CategoryId;
            try
            {
                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteTask();
                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse(ex.Message);
            }
        }
    }
}
