using Microsoft.AspNetCore.Authorization;

namespace ELearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModulesController(IUnitOfWrok unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWrok _unitOfWork = unitOfWork;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllModules()
        {
            var modules = await _unitOfWork.Modules.GetAll();

            if (!modules.Any())
                return NotFound("Modules Not Found");

            var modulesDto = modules.Select(ModuleMapping.MapToDto).ToList();

            return Ok(modulesDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllModulesByCourseDescription([FromQuery] string descriptionCourse)
        {
            var modules = await _unitOfWork.Modules.GetAllByCourseDescription(descriptionCourse);

            if (!modules.Any())
                return NotFound("Modules Not Found");

            var modulesDto = modules.Select(ModuleMapping.MapToDto).ToList();

            return Ok(modulesDto);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModuleById(int id)
        {
            var module = await _unitOfWork.Modules.GetById(id);

            if (module is null)
                return NotFound("Module Not Found");

            var moduleDto = ModuleMapping.MapToDto(module);

            return Ok(moduleDto);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpPost]
        public async Task<IActionResult> AddModule(ModuleDtoRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var allModules = await _unitOfWork.Modules.GetAll();

            var newModule = allModules.Where(x => x.Title == model.Title).FirstOrDefault();

            if (newModule is not null)
            {
                ModelState.AddModelError("Error", "Module Title Already Exist");
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }

            var module = ModuleMapping.MapToEntity(model);

            await _unitOfWork.Modules.Add(module);

            await _unitOfWork.CompleteAsync();

            return Ok(model);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModule(int id, [FromBody] ModuleDtoRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var module = ModuleMapping.MapToEntity(model);
            module.Id = id;

            var result = await _unitOfWork.Modules.Update(module);

            if (result is null)
            {
                ModelState.AddModelError("Error", "Problem When Updating");
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }

            await _unitOfWork.CompleteAsync();

            return Ok(model);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var result = await _unitOfWork.Modules.Delete(id);

            if (result is false)
            {
                ModelState.AddModelError("Error", "Problem When Deleting");
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }

            await _unitOfWork.CompleteAsync();

            return Ok(result);
        }

    }
}
