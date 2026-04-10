using ELearning.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ELearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GradesController(IUnitOfWrok unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWrok _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAllGrades()
        {
            var grades = await _unitOfWork.Grades.GetAll();
            
            if(grades is null)
                return NotFound("Grades Not Found");

            var gradesDto = grades.Select(GradeMapping.MapToDto).ToList();

            return Ok(gradesDto);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeById(int id)
        {
            var grade = await _unitOfWork.Grades.GetById(id);

            if (grade is null)
                return NotFound("Grade Not Found");

            var gradeDto = GradeMapping.MapToDto(grade);

            return Ok(gradeDto);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpPost]
        public async Task<IActionResult> AddGrade(GradeDto model)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var allGrades = await _unitOfWork.Grades.GetAll();

            var newGrade = allGrades.Where(x => x.Name == model.Name).FirstOrDefault();

            if(newGrade is not null)
            {
                ModelState.AddModelError("Error", "Grade Already Exist");
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }

            var grade = GradeMapping.MapToEntity(model);

            await _unitOfWork.Grades.Add(grade);

            await _unitOfWork.CompleteAsync();

            return Ok(model);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] GradeDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            model.Id = id;
            var grade = GradeMapping.MapToEntity(model);

            var result = await _unitOfWork.Grades.Update(grade);

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
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var result = await _unitOfWork.Grades.Delete(id);

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
