using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public ManufacturerService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<Manufacturer>>> GetManufacturersCollection(int? page, int? itemsPerPage)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = (page.Value - 1) * itemsPerPage.Value;
            int endIndex = page.Value * itemsPerPage.Value;
            IEnumerable<Manufacturer> manufacturers;
            Expression<Func<Manufacturer, bool>> predicate = m => m.Id >= startIndex && m.Id < endIndex;

            try
            {
                manufacturers = await _dbService.ManufacturerTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<Manufacturer>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<Manufacturer>>(manufacturers);
        }

        public async Task<ResponseData<Manufacturer>> GetManufacturerById(int id)
        {
            Manufacturer manufacturer;
            try
            {
                manufacturer = await _dbService.ManufacturerTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<Manufacturer>(false, ex.Message);
            }

            return new ResponseData<Manufacturer>(manufacturer);
        }

        public async Task<ResponseData<bool>> AddManufacturer(Manufacturer manufacturer)
        {
            try
            {
                await _dbService.ManufacturerTable.Add(manufacturer);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdateManufacturer(Manufacturer manufacturer, int prevId)
        {
            try
            {
                await _dbService.ManufacturerTable.Update(manufacturer, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeleteManufacturer(int Id)
        {
            try
            {
                await _dbService.ManufacturerTable.Delete(Id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
