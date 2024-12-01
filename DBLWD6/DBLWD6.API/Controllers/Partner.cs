namespace DBLWD6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCollection([FromQuery] int? page, [FromQuery] int? itemsPerPage)
        {
            ResponseData<IEnumerable<Partner>> partnerGetCollectionResponse
                = await _partnerService.GetPartnersCollection(page, itemsPerPage);
            
            if(!partnerGetCollectionResponse.Success)
                return StatusCode(500, partnerGetCollectionResponse.ErrorMessage);

            return Ok(partnerGetCollectionResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<Partner> partnerGetByIdResponse
                = await _partnerService.GetPartnerById(id);

            if (!partnerGetByIdResponse.Success)
                return StatusCode(500, partnerGetByIdResponse.ErrorMessage);

            return Ok(partnerGetByIdResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Partner newPartner)
        {
            ResponseData<bool> partnerCreateResponse
                = await _partnerService.AddPartner(newPartner);

            if (!partnerCreateResponse.Success)
                return StatusCode(500, partnerCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Partner newPartner, int prevId)
        {
            ResponseData<bool> partnerUpdateResponse
                = await _partnerService.UpdatePartner(newPartner, prevId);

            if (!partnerUpdateResponse.Success)
                return StatusCode(500, partnerUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> partnerDeleteResponse
                = await _partnerService.DeletePartner(id);

            if (!partnerDeleteResponse.Success)
                return StatusCode(500, partnerDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
