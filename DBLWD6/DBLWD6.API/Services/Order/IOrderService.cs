namespace DBLWD6.API.Services
{
    public interface IOrderService
    {
        Task<ResponseData<IEnumerable<Order>>> GetOrdersCollection(int? page, int? itemsPerPage, int? userId = null, bool? includeProduct = false, bool? includeUser = false, bool? includePickupPoint = false, bool? includePromoCode = false);
        Task<ResponseData<Order>> GetOrderById(int id, bool? includeProduct = false, bool? includeUser = false, bool? includePickupPoint = false, bool? includePromoCode = false);
        Task<ResponseData<bool>> AddOrder(Order order);
        Task<ResponseData<bool>> UpdateOrder(Order order, int prevId);
        Task<ResponseData<bool>> DeleteOrder(int Id);
    }
}
