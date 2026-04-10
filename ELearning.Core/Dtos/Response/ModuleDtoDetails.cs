namespace ELearning.Core.Dtos.Response
{
    public class ModuleDtoDetails
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public string CourseDescription { get; set; } = string.Empty;
        public string CourseGrade { get; set; } = string.Empty;
        public virtual ICollection<Lesson> Lessons { get; set; } = [];
    }
}
