using Microsoft.AspNetCore.Authorization;

namespace ELearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UsersController(IUnitOfWrok unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWrok _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Users = await _unitOfWork.Users.GetAllUsersAsync();

            if (Users is null)
                return BadRequest("No Users Found");

            var usersDto = Users.Select(UserMapping.MapToDto).ToList();

            return Ok(usersDto);
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserById(string UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var User = await _unitOfWork.Users.GetUserByIdAsync(UserId);

            if (User is null)
                return BadRequest("Not User Exist By This UserId");

            var userDto = UserMapping.MapToDto(User);

            return Ok(userDto);
        }

        [HttpGet("{Username}")]
        public async Task<IActionResult> GetUserByName(string Username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var User = await _unitOfWork.Users.GetUserByNameAsync(Username);

            if (User is null)
                return BadRequest("Not User Exist By This UserName");

            var userDto = UserMapping.MapToDto(User);

            return Ok(userDto);
        }

        [HttpGet("{Useremail}")]
        public async Task<IActionResult> GetUserByEmail(string Useremail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var User = await _unitOfWork.Users.GetUserByEmailAsync(Useremail);

            if (User is null)
                return BadRequest("Not User Exist By This User Emails");

            var userDto = UserMapping.MapToDto(User);

            return Ok(userDto);
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserWithRoles(string UserId)
        {
            var result = await _unitOfWork.Users.GetUserWithRolesAsync(UserId);

            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
