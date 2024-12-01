namespace DBLWD6.API.Services
{
    public interface ISupplierService
    {
        Task<ResponseData<IEnumerable<Supplier>>> GetSuppliersCollection(int? page, int? itemsPerPage);
        Task<ResponseData<Supplier>> GetSupplierById(int id);
        Task<ResponseData<bool>> AddSupplier(Supplier supplier);
        Task<ResponseData<bool>> UpdateSupplier(Supplier supplier, int prevId);
        Task<ResponseData<bool>> DeleteSupplier(int id);
    }
}
