using Microsoft.AspNetCore.Authorization;

namespace ELearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(IUnitOfWrok unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWrok _unitOfWork = unitOfWork;

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.Auths.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.Auths.LoginAsync(model);

            if (!result.IsAuthenticated)
                return Unauthorized(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _unitOfWork.Auths.GetAllRolesAsync();

            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{RoleId}")]
        public async Task<IActionResult> GetRoleById(string RoleId)
        {
            var result = await _unitOfWork.Auths.GetRoleByIdAsync(RoleId);

            if (result is null)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.Auths.CreateRoleAsync(model);

            if (result.Contains("wrong"))
                return BadRequest(result);

            return StatusCode(201, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AssignRole(UserRolesModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.Auths.AssignRoleAsync(model);

            if (result.Contains("Invalid"))
                return BadRequest(result);

            return StatusCode(200, result);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{RoleId}")]
        public async Task<IActionResult> UpdateRole(RoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.Auths.UpdateRoleAsync(model);

            if (result.Contains("wrong"))
                return BadRequest(result);

            return StatusCode(202, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{RoleId}")]
        public async Task<IActionResult> DeleteRole(string RoleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.Auths.DeleteRoleAsync(RoleId);

            if (result is false)
                return BadRequest(result);

            return StatusCode(200, result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.Auths.ChangePasswordAsync(model);

            if (result is false)
                return Unauthorized(result);

            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.Auths.ForgetPasswordAsync(model);

            if (result is false)
                return BadRequest(result);

            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.Auths.ResetPasswordAsync(model);

            if (result is false)
                return BadRequest(result);

            return StatusCode(200, result);
        }
    }
}
