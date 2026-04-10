namespace ELearning.Core.Entities
{
    public class Grade
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Course>? Courses { get; set; } = [];
    }
}
