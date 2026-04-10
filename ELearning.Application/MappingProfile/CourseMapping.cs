namespace ELearning.Application.MappingProfile
{
    public class CourseMapping
    {
        public static CourseDtoDetails MapToDto(Course course)
        {
            if (course is null)
                return null;

            return new CourseDtoDetails
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Grade = course.Grade.Name,
            };
        }

        public static Course MapToEntity(CourseDtoRequest courseDto)
        {
            if (courseDto is null)
                return null;

            return new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                GradeId = courseDto.GradeId,
            };
        }
    }
}
