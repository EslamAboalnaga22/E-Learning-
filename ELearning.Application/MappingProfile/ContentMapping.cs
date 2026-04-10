namespace ELearning.Application.MappingProfile
{
    public class ContentMapping
    {
        public static ContentDtoDetails MapToDto(Content content)
        {
            if (content is null)
                return null;

            return new ContentDtoDetails
            {
                Id = content.Id,
                Title = content.Title,
                Type = content.Type,
                FilePath = content.FilePath,
                LessonTitle = content.Lesson.Title
            };
        }

        public static Content MapToEntity(ContentCreateDtoRequest contentDto)
        {
            if (contentDto is null)
                return null;

            return new Content
            {
                Title = contentDto.Title,
                Type = contentDto.Type,
                LessonId = contentDto.LessonId,
            };
        }
        public static Content MapToEntity(ContentUpdateDtoRequest contentDto)
        {
            if (contentDto is null)
                return null;

            return new Content
            {
                Title = contentDto.Title,
                Type = contentDto.Type,
                LessonId = contentDto.LessonId,
            };
        }
    }
}
