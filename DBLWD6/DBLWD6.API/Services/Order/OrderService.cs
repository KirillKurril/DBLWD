using System.Linq.Expressions;

namespace DBLWD6.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly DbService _dbService;
        private readonly IConfiguration _configuration;

        public OrderService(DbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }

        public async Task<ResponseData<IEnumerable<Order>>> GetOrdersCollection(int? page, int? itemsPerPage, int? userId = null, bool? includeProduct = false, bool? includeUser = false, bool? includePickupPoint = false, bool? includePromoCode = false)
        {
            itemsPerPage = itemsPerPage ?? int.Parse(_configuration.GetSection("ItemsPerPageDefault").Value!);
            page = page ?? 1;
            int startIndex = page.Value * itemsPerPage.Value;
            int endIndex = (page.Value + 1) * itemsPerPage.Value;
            IEnumerable<Order> orders;

            try
            {
                if (userId.HasValue)
                {
                    Expression<Func<Order, bool>> predicate = o => o.Id >= startIndex && o.Id < endIndex && o.UserId == userId.Value;
                    orders = await _dbService.OrderTable.GetWithConditions(predicate);
                }
                else
                {
                    Expression<Func<Order, bool>> predicate = o => o.Id >= startIndex && o.Id < endIndex;
                    orders = await _dbService.OrderTable.GetWithConditions(predicate);
                }

                includeProduct = includeProduct ?? false;
                includeUser = includeUser ?? false;
                includePickupPoint = includePickupPoint ?? false;
                includePromoCode = includePromoCode ?? false;

                foreach (var order in orders)
                {
                    if (includeProduct.Value)
                    {
                        order.Product = await _dbService.ProductTable.GetById(order.ProductId);
                    }
                    if (includeUser.Value)
                    {
                        order.User = await _dbService.UserTable.GetById(order.UserId);
                    }
                    if (includePickupPoint.Value)
                    {
                        order.PickupPoint = await _dbService.PickupPointTable.GetById(order.PickupPointId);
                    }
                    if (includePromoCode.Value && order.PromoCodeId.HasValue)
                    {
                        order.PromoCode = await _dbService.PromoCodeTable.GetById(order.PromoCodeId.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseData<IEnumerable<Order>>(false, ex.Message);
            }

            return new ResponseData<IEnumerable<Order>>(orders);
        }

        public async Task<ResponseData<Order>> GetOrderById(int id, bool? includeProduct = false, bool? includeUser = false, bool? includePickupPoint = false, bool? includePromoCode = false)
        {
            Order order;
            try
            {
                order = await _dbService.OrderTable.GetById(id);

                includeProduct = includeProduct ?? false;
                includeUser = includeUser ?? false;
                includePickupPoint = includePickupPoint ?? false;
                includePromoCode = includePromoCode ?? false;

                if (includeProduct.Value)
                {
                    order.Product = await _dbService.ProductTable.GetById(order.ProductId);
                }
                if (includeUser.Value)
                {
                    order.User = await _dbService.UserTable.GetById(order.UserId);
                }
                if (includePickupPoint.Value)
                {
                    order.PickupPoint = await _dbService.PickupPointTable.GetById(order.PickupPointId);
                }
                if (includePromoCode.Value && order.PromoCodeId.HasValue)
                {
                    order.PromoCode = await _dbService.PromoCodeTable.GetById(order.PromoCodeId.Value);
                }
            }
            catch (Exception ex)
            {
                return new ResponseData<Order>(false, ex.Message);
            }

            return new ResponseData<Order>(order);
        }

        public async Task<ResponseData<bool>> AddOrder(Order order)
        {
            try
            {
                await _dbService.OrderTable.Add(order);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> UpdateOrder(Order order, int prevId)
        {
            try
            {
                await _dbService.OrderTable.Update(order, prevId);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }

        public async Task<ResponseData<bool>> DeleteOrder(int Id)
        {
            try
            {
                await _dbService.OrderTable.Delete(Id);
            }
            catch (Exception ex)
            {
                return new ResponseData<bool>(false, ex.Message);
            }

            return new ResponseData<bool>(true);
        }
    }
}
