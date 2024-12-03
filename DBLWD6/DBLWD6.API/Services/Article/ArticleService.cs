using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class ArticleService : IArticleService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public ArticleService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<Article>>> GetArticlesCollection(int? page, int? itemsPerPage)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = (page.Value - 1) * itemsPerPage.Value;
            int endIndex = page.Value * itemsPerPage.Value;
            IEnumerable<Article> articles;
            Expression<Func<Article, bool>> predicate = (a => a.Id >= startIndex && a.Id < endIndex);

            try
            {
                articles = await _dbService.ArticleTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<Article>>(false, ex.Message);
            }
            
            return new ResponseData<IEnumerable<Article>>(articles);
        }

        public async Task<ResponseData<Article>> GetArticleById(int id)
        {
            Article article;
            try
            {
                article = await _dbService.ArticleTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<Article>(false, ex.Message);
            }

            return new ResponseData<Article>(article);
        }

        public async Task<ResponseData<bool>> AddArticle(Article article)
        {
            try
            {
                await _dbService.ArticleTable.Add(article);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdateArticle(Article article, int prevId)
        {
            try
            {
                await _dbService.ArticleTable.Update(article, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeleteArticle(int Id)
        {
            try
            {
                await _dbService.ArticleTable.Delete(Id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
