namespace ELearning.Core.Dtos.Response
{
    public class CourseDtoDetails
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        //public virtual ICollection<Module> Modules { get; set; } = [];
        //public virtual Enrollment? Enrollment { get; set; }
    }
}
