using DBLWD6.API.Services;
using DBLWD6.Domain.Entities;
using DBLWD6.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBLWD6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;

        public VacancyController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? page, [FromQuery] int? itemsPerPage)
        {
            ResponseData<IEnumerable<Vacancy>> vacanciesResponse
                = await _vacancyService.GetVacanciesCollection(page, itemsPerPage);

            if (!vacanciesResponse.Success)
                return StatusCode(500, vacanciesResponse.ErrorMessage);

            return Ok(vacanciesResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<Vacancy> vacancyResponse
                = await _vacancyService.GetVacancyById(id);

            if (!vacancyResponse.Success)
                return StatusCode(500, vacancyResponse.ErrorMessage);

            if (vacancyResponse.Data == null)
                return NotFound();

            return Ok(vacancyResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vacancy newVacancy)
        {
            ResponseData<bool> vacancyCreateResponse
                = await _vacancyService.AddVacancy(newVacancy);

            if (!vacancyCreateResponse.Success)
                return StatusCode(500, vacancyCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Vacancy newVacancy, int prevId)
        {
            ResponseData<bool> vacancyUpdateResponse
                = await _vacancyService.UpdateVacancy(newVacancy, prevId);

            if (!vacancyUpdateResponse.Success)
                return StatusCode(500, vacancyUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> vacancyDeleteResponse
                = await _vacancyService.DeleteVacancy(id);

            if (!vacancyDeleteResponse.Success)
                return StatusCode(500, vacancyDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
