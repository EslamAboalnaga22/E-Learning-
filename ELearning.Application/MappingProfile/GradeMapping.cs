namespace ELearning.Application.Mapping
{
    public class GradeMapping
    {
        public static GradeDto MapToDto(Grade grade)
        {
            if (grade is null)
                return null;

            return new GradeDto
            {
                Id = grade.Id,
                Name = grade.Name,
            };
        }

        public static Grade MapToEntity(GradeDto gradeDto)
        {
            if (gradeDto is null)
                return null;

            return new Grade
            {
                Id = gradeDto.Id,
                Name = gradeDto.Name,
            };
        }
    }
}
