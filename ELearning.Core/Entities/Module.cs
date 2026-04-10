namespace ELearning.Core.Entities
{
    public class Module
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; } = [];
    }
}
