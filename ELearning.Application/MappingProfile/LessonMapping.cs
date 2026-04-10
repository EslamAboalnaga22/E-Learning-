namespace ELearning.Application.MappingProfile
{
    public class LessonMapping
    {
        public static LessonDtoDetails MapToDto(Lesson lesson)
        {
            if (lesson is null)
                return null;
            var x = lesson.Contents.Where(x => x.Id == lesson.Id).ToList();
            return new LessonDtoDetails
            {
                Id = lesson.Id,
                Title = lesson.Title,
                ModuleTitle = lesson.Module.Title,
                Contents = lesson.Contents.Where(x => x.LessonId == lesson.Id)
                            .Select(x => new Content 
                                { Id = x.Id,
                                  Title = x.Title,
                                  Type = x.Type,
                                  FilePath = x.FilePath,
                                  LessonId = x.LessonId})
                            .ToList(),
            };
        }

        public static Lesson MapToEntity(LessonDtoRequest lessonDto)
        {
            if (lessonDto is null)
                return null;

            return new Lesson
            {
                Title = lessonDto.Title,
                ModuleId = lessonDto.ModuleId,
            };
        }
    }
}
