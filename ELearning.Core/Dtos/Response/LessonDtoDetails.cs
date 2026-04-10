namespace ELearning.Core.Dtos.Response
{
    public class LessonDtoDetails
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ModuleTitle { get; set; } = string.Empty;
        //public virtual Module? Module { get; set; }
        public virtual ICollection<Content> Contents { get; set; } = [];
    }
}
