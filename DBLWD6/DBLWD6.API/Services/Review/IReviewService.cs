namespace DBLWD6.API.Services
{
    public interface IReviewService
    {
        Task<ResponseData<IEnumerable<Review>>> GetReviewsCollection(int? page, int? itemsPerPage);
        Task<ResponseData<Review>> GetReviewById(int id);
        Task<ResponseData<bool>> AddReview(Review review);
        Task<ResponseData<bool>> UpdateReview(Review review, int prevId);
        Task<ResponseData<bool>> DeleteReview(int id);
    }
}
