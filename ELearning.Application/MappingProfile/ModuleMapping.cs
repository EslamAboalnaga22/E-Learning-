namespace ELearning.Application.MappingProfile
{
    public class ModuleMapping
    {
        public static ModuleDtoDetails MapToDto(Module module)
        {
            if (module is null)
                return null;

            return new ModuleDtoDetails
            {
                Id = module.Id,
                Title = module.Title,
                Description = module.Description,
                CourseTitle = module.Course.Title,
                CourseDescription = module.Course.Description,
                CourseGrade = module.Course.Grade.Name,
                Lessons = module.Lessons.Where(x => x.ModuleId == module.Id)
                            .Select (x => new Lesson
                            {
                                Title = x.Title,
                            })
                            .ToList()
            };
        }

        public static Module MapToEntity(ModuleDtoRequest moduleDto)
        {
            if (moduleDto is null)
                return null;

            return new Module
            {
                Title = moduleDto.Title,
                Description= moduleDto.Description,
                CourseId = moduleDto.CourseId,
            };
        }
    }
}
