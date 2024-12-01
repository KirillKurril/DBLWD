using DBLWD6.API.Services;

namespace DBLWD6.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? page, [FromQuery] int? itemsPerPage)
        {
            ResponseData<IEnumerable<Supplier>> suppliersResponse
                = await _supplierService.GetSuppliersCollection(page, itemsPerPage);

            if (!suppliersResponse.Success)
                return StatusCode(500, suppliersResponse.ErrorMessage);

            return Ok(suppliersResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<Supplier> supplierResponse
                = await _supplierService.GetSupplierById(id);

            if (!supplierResponse.Success)
                return StatusCode(500, supplierResponse.ErrorMessage);

            if (supplierResponse.Data == null)
                return NotFound();

            return Ok(supplierResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier newSupplier)
        {
            ResponseData<bool> supplierCreateResponse
                = await _supplierService.AddSupplier(newSupplier);

            if (!supplierCreateResponse.Success)
                return StatusCode(500, supplierCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Supplier newSupplier, int prevId)
        {
            ResponseData<bool> supplierUpdateResponse
                = await _supplierService.UpdateSupplier(newSupplier, prevId);

            if (!supplierUpdateResponse.Success)
                return StatusCode(500, supplierUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> supplierDeleteResponse
                = await _supplierService.DeleteSupplier(id);

            if (!supplierDeleteResponse.Success)
                return StatusCode(500, supplierDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
