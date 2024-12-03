using DBLWD6.API.Services;
using DBLWD6.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DBLWD6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCollection(
            [FromQuery] int? page,
            [FromQuery] int? itemsPerPage,
            [FromQuery] int? userId,
            [FromQuery] bool? includeProduct,
            [FromQuery] bool? includeUser,
            [FromQuery] bool? includePickupPoint,
            [FromQuery] bool? includePromoCode)
        {
            ResponseData<IEnumerable<Order>> orderGetCollectionResponse
                = await _orderService.GetOrdersCollection(page, itemsPerPage, userId, includeProduct, includeUser, includePickupPoint, includePromoCode);

            if (!orderGetCollectionResponse.Success)
                return StatusCode(500, orderGetCollectionResponse.ErrorMessage);

            return Ok(orderGetCollectionResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(
            int id,
            [FromQuery] bool? includeProduct,
            [FromQuery] bool? includeUser,
            [FromQuery] bool? includePickupPoint,
            [FromQuery] bool? includePromoCode)
        {
            ResponseData<Order> orderGetByIdResponse
                = await _orderService.GetOrderById(id, includeProduct, includeUser, includePickupPoint, includePromoCode);

            if (!orderGetByIdResponse.Success)
                return StatusCode(500, orderGetByIdResponse.ErrorMessage);

            return Ok(orderGetByIdResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order newOrder)
        {
            ResponseData<bool> orderCreateResponse
                = await _orderService.AddOrder(newOrder);

            if (!orderCreateResponse.Success)
                return StatusCode(500, orderCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Order newOrder, int prevId)
        {
            ResponseData<bool> orderUpdateResponse
                = await _orderService.UpdateOrder(newOrder, prevId);

            if (!orderUpdateResponse.Success)
                return StatusCode(500, orderUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> orderDeleteResponse
                = await _orderService.DeleteOrder(id);

            if (!orderDeleteResponse.Success)
                return StatusCode(500, orderDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
