using DBLWD6.API.Services;

namespace DBLWD6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCollection([FromQuery] int? page, [FromQuery] int? itemsPerPage)
        {
            ResponseData<IEnumerable<Manufacturer>> manufacturerGetCollectionResponse
                = await _manufacturerService.GetManufacturersCollection(page, itemsPerPage);
            
            if(!manufacturerGetCollectionResponse.Success)
                return StatusCode(500, manufacturerGetCollectionResponse.ErrorMessage);

            return Ok(manufacturerGetCollectionResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<Manufacturer> manufacturerGetByIdResponse
                = await _manufacturerService.GetManufacturerById(id);

            if (!manufacturerGetByIdResponse.Success)
                return StatusCode(500, manufacturerGetByIdResponse.ErrorMessage);

            return Ok(manufacturerGetByIdResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Manufacturer newManufacturer)
        {
            ResponseData<bool> manufacturerCreateResponse
                = await _manufacturerService.AddManufacturer(newManufacturer);

            if (!manufacturerCreateResponse.Success)
                return StatusCode(500, manufacturerCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Manufacturer newManufacturer, int prevId)
        {
            ResponseData<bool> manufacturerUpdateResponse
                = await _manufacturerService.UpdateManufacturer(newManufacturer, prevId);

            if (!manufacturerUpdateResponse.Success)
                return StatusCode(500, manufacturerUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> manufacturerDeleteResponse
                = await _manufacturerService.DeleteManufacturer(id);

            if (!manufacturerDeleteResponse.Success)
                return StatusCode(500, manufacturerDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
