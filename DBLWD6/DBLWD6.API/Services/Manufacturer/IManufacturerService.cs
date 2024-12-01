using DBLWD6.API.Models;
namespace DBLWD6.API.Services
{
    public interface IManufacturerService
    {
        Task<ResponseData<IEnumerable<Manufacturer>>> GetManufacturersCollection(int? page, int? itemsPerPage);
        Task<ResponseData<Manufacturer>> GetManufacturerById(int id);
        Task<ResponseData<bool>> AddManufacturer(Manufacturer manufacturer);
        Task<ResponseData<bool>> UpdateManufacturer(Manufacturer manufacturer, int prevId);
        Task<ResponseData<bool>> DeleteManufacturer(int Id);
    }
}
