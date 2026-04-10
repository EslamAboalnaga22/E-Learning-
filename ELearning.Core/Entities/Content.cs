namespace ELearning.Core.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public int LessonId { get; set; }
        public virtual Lesson? Lesson { get; set; }

    }
}
