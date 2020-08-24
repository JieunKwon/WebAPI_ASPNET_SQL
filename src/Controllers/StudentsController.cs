using StudentGradeAPI.Entities;
using StudentGradeAPI.Models;
using StudentGradeAPI.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentGradeAPI.Controllers
{
    /// <summary>
    /// -- Challenge 2 --
    /// Add an endpoint to return a list of students and their GPAs for Get Request 
    /// </summary> 
    public class StudentsController : ApiController
    { 
        // GET: api/students
        public IEnumerable<Student> GetStudents()
        {
            using (SchoolDBContext dbContext = new SchoolDBContext())
            {
                // list for valid students 
                List<Student> students = new List<Student>();

                // dataContext
                var people = dbContext.People; 

                // build students' list 
                foreach (var person in people)
                {
                    Student student = new Student();
                    if (person.Discriminator.Equals("Student"))
                    { 
                        student.StudentId = person.PersonID;
                        student.FirstName = person.FirstName;
                        student.LastName = person.LastName;
                        // For a student's GPA, call the library to calculate gpa with StudentGrades data
                        CalculateGpa calculateGpa = new CalculateGpa(person.StudentGrades);
                        student.Gpa = calculateGpa.CalStudentGpa();
                        students.Add(student);
                    }
                } 
                return students; 
            }
        } 
    }
}
