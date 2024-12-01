namespace DBLWD6.API.Services
{
    public interface IPickupPointService
    {
        Task<ResponseData<IEnumerable<PickupPoint>>> GetPickupPointsCollection(int? page, int? itemsPerPage);
        Task<ResponseData<PickupPoint>> GetPickupPointById(int id);
        Task<ResponseData<bool>> AddPickupPoint(PickupPoint pickupPoint);
        Task<ResponseData<bool>> UpdatePickupPoint(PickupPoint pickupPoint, int prevId);
        Task<ResponseData<bool>> DeletePickupPoint(int Id);
    }
}
