using DBLWD6.API.Services;
using DBLWD6.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DBLWD6.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? page, [FromQuery] int? itemsPerPage)
        {
            ResponseData<IEnumerable<User>> usersResponse
                = await _userService.GetUsersCollection(page, itemsPerPage);

            if (!usersResponse.Success)
                return StatusCode(500, usersResponse.ErrorMessage);

            return Ok(usersResponse.Data);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData<User> userResponse
                = await _userService.GetUserById(id);

            if (!userResponse.Success)
                return StatusCode(500, userResponse.ErrorMessage);

            if (userResponse.Data == null)
                return NotFound();

            return Ok(userResponse.Data);
        }

        [HttpGet]
        [Route("byEmail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            ResponseData<User> userResponse
                = await _userService.GetUserByEmail(email);

            if (!userResponse.Success)
                return StatusCode(500, userResponse.ErrorMessage);

            if (userResponse.Data == null)
                return NotFound();

            return Ok(userResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User newUser)
        {
            ResponseData<bool> userCreateResponse
                = await _userService.AddUser(newUser);

            if (!userCreateResponse.Success)
                return StatusCode(500, userCreateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(User newUser, int prevId)
        {
            ResponseData<bool> userUpdateResponse
                = await _userService.UpdateUser(newUser, prevId);

            if (!userUpdateResponse.Success)
                return StatusCode(500, userUpdateResponse.ErrorMessage);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseData<bool> userDeleteResponse
                = await _userService.DeleteUser(id);

            if (!userDeleteResponse.Success)
                return StatusCode(500, userDeleteResponse.ErrorMessage);

            return NoContent();
        }

        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> ValidateCredentials([FromBody] LoginModel model)
        {
            ResponseData<bool> validationResponse
                = await _userService.ValidateCredentials(model.Email, model.Password);

            if (!validationResponse.Success)
                return StatusCode(401, validationResponse.ErrorMessage);

            return Ok();
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
