namespace ELearning.Core.Dtos.Request
{
    public class ContentUpdateDtoRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Type { get; set; } = string.Empty;
        public string? FilePath { get; set; } = string.Empty;

        //public IFormFile? NewFile { get; set; }
        public byte[]? NewFile { get; set; }
        public int LessonId { get; set; }
    }
}
