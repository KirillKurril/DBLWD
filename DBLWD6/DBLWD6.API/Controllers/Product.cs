using DBLWD6.API.Models;
using DBLWD6.API.Services;
using DBLWD6.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DBLWD6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        DbService _dbService;
        JsonSerializerOptions _options;
        IProductService _productService;
        public ProductController(DbService service, JsonSerializerOptions options, IProductService productService)
        {
            _dbService = service;
            _options = options;
            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCollection([FromQuery] int? page, [FromQuery] int? itemsPerPage, [FromQuery] int? categoryId)
        {
            ResponseData<IEnumerable<Product>> productGetCollectionResponse
                = await _productService.GetProductsCollection(page, itemsPerPage, categoryId);
            
            if(!productGetCollectionResponse.Success)
                return StatusCode(500, productGetCollectionResponse.ErrorMessage);

            return Ok(productGetCollectionResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<Product> productGetByIdResponse
                = await _productService.GetProductById(id);

            if (!productGetByIdResponse.Success)
                return StatusCode(500, productGetByIdResponse.ErrorMessage);

            return Ok(productGetByIdResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product newProduct)
        {
            ResponseData<bool> productCreateResponse
                = await _productService.AddProduct(newProduct);

            if (!productCreateResponse.Success)
                return StatusCode(500, productCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product newProduct, int prevId)
        {
            ResponseData<bool> productUpdateResponse
                = await _productService.UpdateProduct(newProduct, prevId);

            if (!productUpdateResponse.Success)
                return StatusCode(500, productUpdateResponse.ErrorMessage);

            return NoContent();
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> productDeleteResponse
                = await _productService.DeleteProduct(id);

            if (!productDeleteResponse.Success)
                return StatusCode(500, productDeleteResponse.ErrorMessage);

            return NoContent();
        }


    }
}
