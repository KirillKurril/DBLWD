using DBLWD6.API.Services;
using DBLWD6.Domain.Entities;
using DBLWD6.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBLWD6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IFAQService _faqService;

        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCollection([FromQuery] int? page, [FromQuery] int? itemsPerPage, [FromQuery] bool includeArticle = false)
        {
            ResponseData<IEnumerable<FAQ>> faqGetCollectionResponse
                = await _faqService.GetFAQsCollection(page, itemsPerPage, includeArticle);
            
            if(!faqGetCollectionResponse.Success)
                return StatusCode(500, faqGetCollectionResponse.ErrorMessage);

            return Ok(faqGetCollectionResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeArticle = false)
        {
            ResponseData<FAQ> faqGetByIdResponse
                = await _faqService.GetFAQById(id, includeArticle);

            if (!faqGetByIdResponse.Success)
                return StatusCode(500, faqGetByIdResponse.ErrorMessage);

            return Ok(faqGetByIdResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FAQ newFAQ)
        {
            ResponseData<bool> faqCreateResponse
                = await _faqService.AddFAQ(newFAQ);

            if (!faqCreateResponse.Success)
                return StatusCode(500, faqCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(FAQ newFAQ, int prevId)
        {
            ResponseData<bool> faqUpdateResponse
                = await _faqService.UpdateFAQ(newFAQ, prevId);

            if (!faqUpdateResponse.Success)
                return StatusCode(500, faqUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> faqDeleteResponse
                = await _faqService.DeleteFAQ(id);

            if (!faqDeleteResponse.Success)
                return StatusCode(500, faqDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
