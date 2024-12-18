using DBLWD6.API.Models;

namespace DBLWD6.API.Services
{
    public interface IProductService
    {
        Task<ResponseData<IEnumerable<Product>>> GetProductsCollection(int? page, int? itemsPerPage, int? categoryId, bool? includeCategory = false, bool? includeManufacturers = false, bool? includeSuppliers = false, bool? includePickupPoints = false);
        Task<ResponseData<Product>> GetProductById(int id, bool? includeCategory = false, bool? includeManufacturers = false, bool? includeSuppliers = false, bool? includePickupPoints = false);
        Task<ResponseData<bool>> AddProduct(Product product);
        Task<ResponseData<bool>> UpdateProduct(Product product, int prevId);
        Task<ResponseData<bool>> DeleteProduct(int Id);
    }
}
