using DBLWD6.API.Services;
using DBLWD6.Domain.Entities;
using DBLWD6.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBLWD6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCollection([FromQuery] int? page, [FromQuery] int? itemsPerPage)
        {
            ResponseData<IEnumerable<Category>> categoryGetCollectionResponse
                = await _categoryService.GetCategoriesCollection(page, itemsPerPage);
            
            if(!categoryGetCollectionResponse.Success)
                return StatusCode(500, categoryGetCollectionResponse.ErrorMessage);

            return Ok(categoryGetCollectionResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<Category> categoryGetByIdResponse
                = await _categoryService.GetCategoryById(id);

            if (!categoryGetByIdResponse.Success)
                return StatusCode(500, categoryGetByIdResponse.ErrorMessage);

            return Ok(categoryGetByIdResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category newCategory)
        {
            ResponseData<bool> categoryCreateResponse
                = await _categoryService.AddCategory(newCategory);

            if (!categoryCreateResponse.Success)
                return StatusCode(500, categoryCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category newCategory, int prevId)
        {
            ResponseData<bool> categoryUpdateResponse
                = await _categoryService.UpdateCategory(newCategory, prevId);

            if (!categoryUpdateResponse.Success)
                return StatusCode(500, categoryUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> categoryDeleteResponse
                = await _categoryService.DeleteCategory(id);

            if (!categoryDeleteResponse.Success)
                return StatusCode(500, categoryDeleteResponse.ErrorMessage);

            return NoContent();
        }
    }
}
