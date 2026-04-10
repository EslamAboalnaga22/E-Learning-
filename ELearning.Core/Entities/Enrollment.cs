namespace ELearning.Core.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
}
