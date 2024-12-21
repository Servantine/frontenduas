namespace utssapi.Models
{
    public class Enrollments
    {
        public int enrollmentId { get; set; }
        public int? instructorId { get; set; }
        public int? courseId { get; set; }
        public DateTimeOffset EnrolledAt { get; set; }
    }
}
