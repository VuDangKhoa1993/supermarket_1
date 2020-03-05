using Microsoft.Extensions.Caching.Memory;
using SupermarketAPI.Domain.Models;
using SupermarketAPI.Domain.Repositories;
using SupermarketAPI.Domain.Services;
using SupermarketAPI.Domain.Services.Communication;
using SupermarketAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            // Here I try to get the categories list from the memory cache. If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire one minute ahead and returning the Task that lists the categories from the repository.
            var categories = await _cache.GetOrCreateAsync(CacheKeys.CategoriesLists, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _categoryRepository.ListAsync();
            });
            return categories;

        }
        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CompleteTask();
                return new CategoryResponse(category);
            }
            catch(Exception ex)
            {
                return new CategoryResponse(ex.Message);
            }

        }
        public async Task<CategoryResponse> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);
            if(existingCategory == null)
                return new CategoryResponse("Category not found!");

            existingCategory.Name = category.Name;

            try
            {
                _categoryRepository.Update(existingCategory);
                await _unitOfWork.CompleteTask();
                return new CategoryResponse(existingCategory);
            }
            catch(Exception ex)
            {
                return new CategoryResponse(ex.Message);
            }
        }
        public async Task<CategoryResponse> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);
            if (existingCategory == null)
                return new CategoryResponse("Category not found!");
            try
            {
                _categoryRepository.Delete(existingCategory);
                await _unitOfWork.CompleteTask();
                return new CategoryResponse(existingCategory);
               
            }
            catch (Exception ex)
            {
                return new CategoryResponse(ex.Message); 

            }
        }

    }
}
