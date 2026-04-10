namespace ELearning.Core.Dtos.Request
{
    public class CourseDtoRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int GradeId { get; set; }
        //public virtual ICollection<Module> Modules { get; set; } = [];
        //public virtual Enrollment? Enrollment { get; set; }
    }
}
