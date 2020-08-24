using StudentGradeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentGradeAPI.Libraries
{
    /// <summary>
    /// 
    /// CalculateGpa : Calculate the student's GPA by studentGrades data 
    /// 
    /// Params: ICollection<StudentGrade> studentGrades
    /// 
    /// Formula: GPA = (Total Grades) / (Total Credits) 
    ///     
    /// Null grade will not be included for GPA
    /// Return value will be rounded to two decimal places (ex: 3.75)
    ///  
    /// </summary> 
    public class CalculateGpa
    {
        private ICollection<StudentGrade> studentGrades;
         
        public CalculateGpa(ICollection<StudentGrade> studentGrades)
        {
            this.studentGrades = studentGrades;
        }

        // return a GPA with student's grades 
        public decimal CalStudentGpa()
        {
            decimal grades = 0.00m;
            int credits = 0;    
            foreach (var g in studentGrades)
            {
                // except NULL value
                if (g.Grade != null)
                {
                    grades += (decimal)g.Grade * g.Course.Credits;
                    credits += g.Course.Credits;
                }
            }

            return credits > 0 ? Math.Round(grades / credits, 2) : grades;
        }
    }
}