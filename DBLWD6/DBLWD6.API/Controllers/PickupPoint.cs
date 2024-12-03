using DBLWD6.API.Services;
using DBLWD6.Domain.Entities;
using DBLWD6.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBLWD6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupPointController : ControllerBase
    {
        private readonly IPickupPointService _pickupPointService;

        public PickupPointController(IPickupPointService pickupPointService)
        {
            _pickupPointService = pickupPointService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCollection([FromQuery] int? page, [FromQuery] int? itemsPerPage)
        {
            ResponseData<IEnumerable<PickupPoint>> pickupPointGetCollectionResponse
                = await _pickupPointService.GetPickupPointsCollection(page, itemsPerPage);
            
            if(!pickupPointGetCollectionResponse.Success)
                return StatusCode(500, pickupPointGetCollectionResponse.ErrorMessage);

            return Ok(pickupPointGetCollectionResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<PickupPoint> pickupPointGetByIdResponse
                = await _pickupPointService.GetPickupPointById(id);

            if (!pickupPointGetByIdResponse.Success)
                return StatusCode(500, pickupPointGetByIdResponse.ErrorMessage);

            return Ok(pickupPointGetByIdResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PickupPoint newPickupPoint)
        {
            ResponseData<bool> pickupPointCreateResponse
                = await _pickupPointService.AddPickupPoint(newPickupPoint);

            if (!pickupPointCreateResponse.Success)
                return StatusCode(500, pickupPointCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(PickupPoint newPickupPoint, int prevId)
        {
            ResponseData<bool> pickupPointUpdateResponse
                = await _pickupPointService.UpdatePickupPoint(newPickupPoint, prevId);

            if (!pickupPointUpdateResponse.Success)
                return StatusCode(500, pickupPointUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> pickupPointDeleteResponse
                = await _pickupPointService.DeletePickupPoint(id);

            if (!pickupPointDeleteResponse.Success)
                return StatusCode(500, pickupPointDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
