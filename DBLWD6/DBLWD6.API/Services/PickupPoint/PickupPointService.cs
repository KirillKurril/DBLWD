using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class PickupPointService : IPickupPointService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public PickupPointService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<PickupPoint>>> GetPickupPointsCollection(int? page, int? itemsPerPage)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = (page.Value - 1) * itemsPerPage.Value;
            int endIndex = page.Value * itemsPerPage.Value;
            IEnumerable<PickupPoint> pickupPoints;
            Expression<Func<PickupPoint, bool>> predicate = p => p.Id >= startIndex && p.Id < endIndex;

            try
            {
                pickupPoints = await _dbService.PickupPointTable.GetWithConditions(predicate);
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<PickupPoint>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<PickupPoint>>(pickupPoints);
        }

        public async Task<ResponseData<PickupPoint>> GetPickupPointById(int id)
        {
            PickupPoint pickupPoint;
            try
            {
                pickupPoint = await _dbService.PickupPointTable.GetById(id);
            }
            catch (Exception ex)
            {
                return new ResponseData<PickupPoint>(false, ex.Message);
            }

            return new ResponseData<PickupPoint>(pickupPoint);
        }

        public async Task<ResponseData<bool>> AddPickupPoint(PickupPoint pickupPoint)
        {
            try
            {
                await _dbService.PickupPointTable.Add(pickupPoint);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdatePickupPoint(PickupPoint pickupPoint, int prevId)
        {
            try
            {
                await _dbService.PickupPointTable.Update(pickupPoint, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeletePickupPoint(int Id)
        {
            try
            {
                await _dbService.PickupPointTable.Delete(Id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
