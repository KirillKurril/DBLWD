using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public SupplierService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<Supplier>>> GetSuppliersCollection(int? page, int? itemsPerPage)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = page.Value * itemsPerPage.Value;
            int endIndex = (page.Value + 1) * itemsPerPage.Value;
            IEnumerable<Supplier> suppliers;
            Expression<Func<Supplier, bool>> predicate = s => s.Id >= startIndex && s.Id < endIndex;

            try
            {
                suppliers = await _dbService.SupplierTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<Supplier>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<Supplier>>(suppliers);
        }

        public async Task<ResponseData<Supplier>> GetSupplierById(int id)
        {
            Supplier supplier;
            try
            {
                supplier = await _dbService.SupplierTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<Supplier>(false, ex.Message);
            }

            return new ResponseData<Supplier>(supplier);
        }

        public async Task<ResponseData<bool>> AddSupplier(Supplier supplier)
        {
            try
            {
                await _dbService.SupplierTable.Add(supplier);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdateSupplier(Supplier supplier, int prevId)
        {
            try
            {
                await _dbService.SupplierTable.Update(supplier, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeleteSupplier(int id)
        {
            try
            {
                await _dbService.SupplierTable.Delete(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
