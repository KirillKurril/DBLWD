using DBLWD6.API.Services;

namespace DBLWD6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCollection(
            [FromQuery] int? page, 
            [FromQuery] int? itemsPerPage, 
            [FromQuery] int? categoryId,
            [FromQuery] bool? includeCategory,
            [FromQuery] bool? includeManufacturers,
            [FromQuery] bool? includeSuppliers,
            [FromQuery] bool? includePickupPoints)
        {
            ResponseData<IEnumerable<Product>> productGetCollectionResponse
                = await _productService.GetProductsCollection(page, itemsPerPage, categoryId, includeCategory, includeManufacturers, includeSuppliers, includePickupPoints);
            
            if(!productGetCollectionResponse.Success)
                return StatusCode(500, productGetCollectionResponse.ErrorMessage);

            return Ok(productGetCollectionResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool? includeCategory, [FromQuery] bool? includeManufacturers, [FromQuery] bool? includeSuppliers, [FromQuery] bool? includePickupPoints)
        {
            ResponseData<Product> productGetByIdResponse
                = await _productService.GetProductById(id, includeCategory, includeManufacturers, includeSuppliers, includePickupPoints);

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
