using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public ReviewService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<Review>>> GetReviewsCollection(int? page, int? itemsPerPage)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = (page.Value - 1) * itemsPerPage.Value;
            int endIndex = page.Value * itemsPerPage.Value;
            IEnumerable<Review> reviews;
            Expression<Func<Review, bool>> predicate;

            try
            {
                predicate = r => r.Id >= startIndex && r.Id < endIndex;
                reviews = await _dbService.ReviewTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<Review>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<Review>>(reviews);
        }

        public async Task<ResponseData<Review>> GetReviewById(int id)
        {
            Review review;
            try
            {
                review = await _dbService.ReviewTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<Review>(false, ex.Message);
            }

            return new ResponseData<Review>(review);
        }

        public async Task<ResponseData<bool>> AddReview(Review review)
        {
            try
            {
                await _dbService.ReviewTable.Add(review);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdateReview(Review review, int prevId)
        {
            try
            {
                await _dbService.ReviewTable.Update(review, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeleteReview(int id)
        {
            try
            {
                await _dbService.ReviewTable.Delete(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
