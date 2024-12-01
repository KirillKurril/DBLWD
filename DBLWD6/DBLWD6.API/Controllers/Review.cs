using DBLWD6.API.Services;

namespace DBLWD6.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? page, [FromQuery] int? itemsPerPage)
        {
            ResponseData<IEnumerable<Review>> reviewsResponse
                = await _reviewService.GetReviewsCollection(page, itemsPerPage);

            if (!reviewsResponse.Success)
                return StatusCode(500, reviewsResponse.ErrorMessage);

            return Ok(reviewsResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<Review> reviewResponse
                = await _reviewService.GetReviewById(id);

            if (!reviewResponse.Success)
                return StatusCode(500, reviewResponse.ErrorMessage);

            if (reviewResponse.Data == null)
                return NotFound();

            return Ok(reviewResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Review newReview)
        {
            ResponseData<bool> reviewCreateResponse
                = await _reviewService.AddReview(newReview);

            if (!reviewCreateResponse.Success)
                return StatusCode(500, reviewCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Review newReview, int prevId)
        {
            ResponseData<bool> reviewUpdateResponse
                = await _reviewService.UpdateReview(newReview, prevId);

            if (!reviewUpdateResponse.Success)
                return StatusCode(500, reviewUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> reviewDeleteResponse
                = await _reviewService.DeleteReview(id);

            if (!reviewDeleteResponse.Success)
                return StatusCode(500, reviewDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
