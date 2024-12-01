namespace DBLWD6.API.Services
{
    public interface IFAQService
    {
        Task<ResponseData<IEnumerable<FAQ>>> GetFAQsCollection(int? page, int? itemsPerPage, bool? includeArticle);
        Task<ResponseData<FAQ>> GetFAQById(int id, bool? includeArticle);
        Task<ResponseData<bool>> AddFAQ(FAQ faq);
        Task<ResponseData<bool>> UpdateFAQ(FAQ faq, int prevId);
        Task<ResponseData<bool>> DeleteFAQ(int Id);
    }
}
