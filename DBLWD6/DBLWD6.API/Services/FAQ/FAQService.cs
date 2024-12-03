using DBLWD6.Domain.Entities;
using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class FAQService : IFAQService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public FAQService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<FAQ>>> GetFAQsCollection(int? page, int? itemsPerPage, bool? includeArticle)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            includeArticle = includeArticle ?? false;
            int startIndex = (page.Value - 1) * itemsPerPage.Value;
            int endIndex = page.Value * itemsPerPage.Value;
            IEnumerable<FAQ> faqs;
            Expression<Func<FAQ, bool>> predicate = f => f.Id >= startIndex && f.Id < endIndex;

            try
            {
                faqs = await _dbService.FAQTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<FAQ>>(false, ex.Message);
            }

            if(includeArticle.Value)
            {
                foreach (var faq in faqs)
                {
                    Article article = await _dbService.ArticleTable.GetById(faq.ArticleId);
                    faq.Article = article;
                }
            }

            return new ResponseData<IEnumerable<FAQ>>(faqs);
        }

        public async Task<ResponseData<FAQ>> GetFAQById(int id, bool? includeArticle)
        {
            FAQ faq;
            try
            {
                faq = await _dbService.FAQTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<FAQ>(false, ex.Message);
            }

            includeArticle = includeArticle ?? false;
            if (includeArticle.Value)
            {
                Article article = await _dbService.ArticleTable.GetById(faq.ArticleId);
                faq.Article = article;
            }

            return new ResponseData<FAQ>(faq);
        }

        public async Task<ResponseData<bool>> AddFAQ(FAQ faq)
        {
            try
            {
                await _dbService.FAQTable.Add(faq);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdateFAQ(FAQ faq, int prevId)
        {
            try
            {
                await _dbService.FAQTable.Update(faq, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeleteFAQ(int Id)
        {
            try
            {
                await _dbService.FAQTable.Delete(Id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
