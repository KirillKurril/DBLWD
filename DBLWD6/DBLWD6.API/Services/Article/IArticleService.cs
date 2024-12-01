namespace DBLWD6.API.Services
{
    public interface IArticleService
    {
        Task<ResponseData<IEnumerable<Article>>> GetArticlesCollection(int? page, int? itemsPerPage);
        Task<ResponseData<Article>> GetArticleById(int id);
        Task<ResponseData<bool>> AddArticle(Article article);
        Task<ResponseData<bool>> UpdateArticle(Article article, int prevId);
        Task<ResponseData<bool>> DeleteArticle(int Id);
    }
}
