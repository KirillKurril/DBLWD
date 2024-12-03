using DBLWD6.API.Services;
using DBLWD6.Domain.Entities;
using DBLWD6.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBLWD6.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromoCodeController : ControllerBase
    {
        private readonly IPromoCodeService _promoCodeService;

        public PromoCodeController(IPromoCodeService promoCodeService)
        {
            _promoCodeService = promoCodeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? page, [FromQuery] int? itemsPerPage)
        {
            ResponseData<IEnumerable<PromoCode>> promoCodesResponse 
                = await _promoCodeService.GetPromoCodesCollection(page, itemsPerPage);

            if (!promoCodesResponse.Success)
                return StatusCode(500, promoCodesResponse.ErrorMessage);

            return Ok(promoCodesResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<PromoCode> promoCodeResponse 
                = await _promoCodeService.GetPromoCodeById(id);

            if (!promoCodeResponse.Success)
                return StatusCode(500, promoCodeResponse.ErrorMessage);

            if (promoCodeResponse.Data == null)
                return NotFound();

            return Ok(promoCodeResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PromoCode newPromoCode)
        {
            ResponseData<bool> promoCodeCreateResponse
                = await _promoCodeService.AddPromoCode(newPromoCode);

            if (!promoCodeCreateResponse.Success)
                return StatusCode(500, promoCodeCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(PromoCode newPromoCode, int prevId)
        {
            ResponseData<bool> promoCodeUpdateResponse
                = await _promoCodeService.UpdatePromoCode(newPromoCode, prevId);

            if (!promoCodeUpdateResponse.Success)
                return StatusCode(500, promoCodeUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> promoCodeDeleteResponse
                = await _promoCodeService.DeletePromoCode(id);

            if (!promoCodeDeleteResponse.Success)
                return StatusCode(500, promoCodeDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
