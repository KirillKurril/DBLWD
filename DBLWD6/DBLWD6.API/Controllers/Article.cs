namespace DBLWD6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCollection([FromQuery] int? page, [FromQuery] int? itemsPerPage)
        {
            ResponseData<IEnumerable<Article>> articleGetCollectionResponse
                = await _articleService.GetArticlesCollection(page, itemsPerPage);
            
            if(!articleGetCollectionResponse.Success)
                return StatusCode(500, articleGetCollectionResponse.ErrorMessage);

            return Ok(articleGetCollectionResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<Article> articleGetByIdResponse
                = await _articleService.GetArticleById(id);

            if (!articleGetByIdResponse.Success)
                return StatusCode(500, articleGetByIdResponse.ErrorMessage);

            return Ok(articleGetByIdResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article newArticle)
        {
            ResponseData<bool> articleCreateResponse
                = await _articleService.AddArticle(newArticle);

            if (!articleCreateResponse.Success)
                return StatusCode(500, articleCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Article newArticle, int prevId)
        {
            ResponseData<bool> articleUpdateResponse
                = await _articleService.UpdateArticle(newArticle, prevId);

            if (!articleUpdateResponse.Success)
                return StatusCode(500, articleUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> articleDeleteResponse
                = await _articleService.DeleteArticle(id);

            if (!articleDeleteResponse.Success)
                return StatusCode(500, articleDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
