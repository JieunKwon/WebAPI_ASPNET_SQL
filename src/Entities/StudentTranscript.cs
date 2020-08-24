using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentGradeAPI.Entities
{
    public class StudentTranscript
    {
        public Student Student { get; set; }
        public List<CourseGrade> Grades { get; set; }
    }
}