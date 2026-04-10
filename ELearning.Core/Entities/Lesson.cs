namespace ELearning.Core.Entities
{
    public class Lesson
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public int ModuleId { get; set; }
        public virtual Module? Module { get; set; }
        public virtual ICollection<Content> Contents { get; set; } = [];
    }
}
