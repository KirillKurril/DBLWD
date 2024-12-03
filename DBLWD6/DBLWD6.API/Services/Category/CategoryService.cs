using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public CategoryService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<Category>>> GetCategoriesCollection(int? page, int? itemsPerPage)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = (page.Value - 1) * itemsPerPage.Value;
            int endIndex = page.Value * itemsPerPage.Value;
            IEnumerable<Category> categories;
            Expression<Func<Category, bool>> predicate = c => c.Id >= startIndex && c.Id < endIndex;

            try
            {
                categories = await _dbService.CategoryTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<Category>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<Category>>(categories);
        }

        public async Task<ResponseData<Category>> GetCategoryById(int id)
        {
            Category category;
            try
            {
                category = await _dbService.CategoryTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<Category>(false, ex.Message);
            }

            return new ResponseData<Category>(category);
        }

        public async Task<ResponseData<bool>> AddCategory(Category category)
        {
            try
            {
                await _dbService.CategoryTable.Add(category);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdateCategory(Category category, int prevId)
        {
            try
            {
                await _dbService.CategoryTable.Update(category, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeleteCategory(int Id)
        {
            try
            {
                await _dbService.CategoryTable.Delete(Id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
