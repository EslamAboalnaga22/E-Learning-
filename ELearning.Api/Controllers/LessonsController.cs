using Microsoft.AspNetCore.Authorization;

namespace ELearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LessonsController(IUnitOfWrok unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWrok _unitOfWork = unitOfWork;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllLessons()
        {
            var lessons = await _unitOfWork.Lessons.GetAll();

            if (!lessons.Any())
                return NotFound("Lessons Not Found");

            var lessonsDto = lessons.Select(LessonMapping.MapToDto).ToList();

            return Ok(lessonsDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllLessonsByModuleTitle([FromQuery] string moduleTitle)
        {
            var lessons = await _unitOfWork.Lessons.GetAllLessonByModuleTitle(moduleTitle);

            if (!lessons.Any())
                return NotFound("Lessons Not Found");

            var lessonsDto = lessons.Select(LessonMapping.MapToDto).ToList();

            return Ok(lessonsDto);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonById(int id)
        {
            var lesson = await _unitOfWork.Lessons.GetById(id);

            if (lesson is null)
                return NotFound("Lesson Not Found");

            var lessonDto = LessonMapping.MapToDto(lesson);

            return Ok(lessonDto);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpPost]
        public async Task<IActionResult> AddLesson(LessonDtoRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var allLessons = await _unitOfWork.Lessons.GetAll();

            var newLesson = allLessons.Where(x => x.Title == model.Title).FirstOrDefault();

            if (newLesson is not null)
            {
                ModelState.AddModelError("Error", "Lesson Title Already Exist");
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }

            var lesson = LessonMapping.MapToEntity(model);

            await _unitOfWork.Lessons.Add(lesson);

            await _unitOfWork.CompleteAsync();

            return Ok(model);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, [FromBody] LessonDtoRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lesson = LessonMapping.MapToEntity(model);
            lesson.Id = id;

            var result = await _unitOfWork.Lessons.Update(lesson);

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
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var result = await _unitOfWork.Lessons.Delete(id);

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
