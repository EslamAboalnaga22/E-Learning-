namespace ELearning.Core.Dtos.Request
{
    public class ModuleDtoRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CourseId { get; set; } 

        //public virtual ICollection<Lesson> Lessons { get; set; } = [];
    }
}
