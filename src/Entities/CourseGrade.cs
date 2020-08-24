using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentGradeAPI.Entities
{
    public class CourseGrade
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public decimal Grade { get; set; }
    }
}