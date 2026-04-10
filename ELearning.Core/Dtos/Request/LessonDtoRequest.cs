namespace ELearning.Core.Dtos.Request
{
    public class LessonDtoRequest
    {
        public string Title { get; set; } = string.Empty;
        public int ModuleId { get; set; }
        //public virtual Module? Module { get; set; }
        //public virtual ICollection<Content> Contents { get; set; } = [];
    }
}
