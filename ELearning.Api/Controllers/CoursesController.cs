using Microsoft.AspNetCore.Authorization;

namespace ELearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoursesController(IUnitOfWrok unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWrok _unitOfWork = unitOfWork;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _unitOfWork.Courses.GetAll();

            if (!courses.Any())
                return NotFound("Courses Not Found");

            var coursesDto = courses.Select(CourseMapping.MapToDto).ToList();

            return Ok(coursesDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllCoursesByGradeId([FromQuery] int gradeId)
        {
            var courses = await _unitOfWork.Courses.GetAllByGradeId(gradeId);

            if (!courses.Any())
                return NotFound("Courses Not Found");

            var coursesDto = courses.Select(CourseMapping.MapToDto).ToList();

            return Ok(coursesDto);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _unitOfWork.Courses.GetById(id);

            if (course is null)
                return NotFound("Course Not Found");

            var courseDto = CourseMapping.MapToDto(course);

            return Ok(courseDto);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseDtoRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var allCourses = await _unitOfWork.Courses.GetAll();

            var newCourse = allCourses.Where(x => x.Title == model.Title).FirstOrDefault();

            if (newCourse is not null)
            {
                ModelState.AddModelError("Error", "Course Title Already Exist");
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }

            var course = CourseMapping.MapToEntity(model);

            await _unitOfWork.Courses.Add(course);

            return Ok(model);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseDtoRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = CourseMapping.MapToEntity(model);
            course.Id = id;

            var result = await _unitOfWork.Courses.Update(course);

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
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _unitOfWork.Courses.Delete(id);

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
