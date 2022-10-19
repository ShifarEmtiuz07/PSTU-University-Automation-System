using System.ComponentModel.DataAnnotations;

namespace PSTU_Automation1.Models
{
    public class Enroll_Course
    {
        [Key]
        public int EnrolId { get; set; }
        public int StudentId { get; set; }
        public string InstructorName { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public int CourseCradit { get; set; }

        public string Department { get; set; }
    }
}
