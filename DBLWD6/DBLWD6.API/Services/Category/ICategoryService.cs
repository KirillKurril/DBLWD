using DBLWD6.API.Models;
namespace DBLWD6.API.Services
{
    public interface ICategoryService
    {
        Task<ResponseData<IEnumerable<Category>>> GetCategoriesCollection(int? page, int? itemsPerPage);
        Task<ResponseData<Category>> GetCategoryById(int id);
        Task<ResponseData<bool>> AddCategory(Category category);
        Task<ResponseData<bool>> UpdateCategory(Category category, int prevId);
        Task<ResponseData<bool>> DeleteCategory(int Id);
    }
}
