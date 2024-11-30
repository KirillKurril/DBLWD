using DBLWD6.API.Models;
using DBLWD6.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class ProductService : IProductService
    {
        DbService _dbService;
        IConfiguration _configuration;
        public ProductService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }
        public async Task<ResponseData<IEnumerable<Product>>> GetProductsCollection(int? page, int? itemsPerPage, int? categoryId)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = page.Value * itemsPerPage.Value;
            int endIndex = (page.Value + 1) * itemsPerPage.Value;
            IEnumerable<Product> products;
            Expression<Func<Product, bool>> predicate;

            if (categoryId == null)
                predicate = (p => p.Id >= startIndex && p.Id < endIndex);
            else
                predicate = (p => p.Id >= startIndex && p.Id < endIndex && p.CategoryId == categoryId);

            try
            {
                products = await _dbService.ProductTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<Product>>(false, ex.Message);
            }
            
            return new ResponseData<IEnumerable<Product>>(products);
        }
        public async Task<ResponseData<Product>> GetProductById(int id)
        {
            Product product;
            try
            {
                product = await _dbService.ProductTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<Product>(false, ex.Message);
            }

            return new ResponseData<Product>(product);
        }
        public async Task<ResponseData<bool>> AddProduct(Product product)
        {
            try
            {
                await _dbService.ProductTable.Add(product);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
        public async Task<ResponseData<bool>> UpdateProduct(Product product, int prevId)
        {
            try
            {
                await _dbService.ProductTable.Update(product, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
        public async Task<ResponseData<bool>> DeleteProduct(int Id)
        {
            try
            {
                await _dbService.ProductTable.Delete(Id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }

}
