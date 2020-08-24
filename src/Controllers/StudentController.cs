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
    /// -- Challenge 1 --
    /// Add an endpoint to retrieve a student's transcript given the ID of the student 
    /// </summary> 
    public class StudentController : ApiController
    {
        // GET: api/student 
        public HttpResponseMessage Get()
        {  
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Need the specific Student ID"); 
        }

        // GET: api/student/{studentId} 
        public HttpResponseMessage GetStudentTranscriptById(int studentId)
        {
            using (SchoolDBContext dbContext = new SchoolDBContext())
            {  
                var person = dbContext.People.Find(studentId);
                
                // Not found
                if (person == null)
                { 
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Student with ID " + studentId.ToString() + " does not exist"); 
                }

                // Not a student
                if (!person.Discriminator.Equals("Student"))
                { 
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "ID " + studentId.ToString() + " is not for a student");
                }

                // create a StudentTranscript
                StudentTranscript studentTranscript = new StudentTranscript();

                // student information for StudentTranscript
                Student student = new Student
                {
                    StudentId = studentId,
                    FirstName = person.FirstName,
                    LastName = person.LastName
                };
                
                // For a student's GPA, call the library to calculate gpa with StudentGrades data
                CalculateGpa calculateGpa = new CalculateGpa(person.StudentGrades);
                student.Gpa = calculateGpa.CalStudentGpa();
                
                // Create Grades' list 
                List<CourseGrade> courseGrades = new List<CourseGrade>(); 
                foreach(var g in person.StudentGrades)
                {
                    // except NULL grades
                    if (g.Grade != null)
                    { 
                        // Course Info
                        var course = dbContext.Courses.FirstOrDefault(e => e.CourseID == g.CourseID);
                        CourseGrade cGrade = new CourseGrade
                        {
                            CourseId = course.CourseID,
                            Title = course.Title,
                            Credits = course.Credits,
                            Grade = (decimal)g.Grade
                        };
                        courseGrades.Add(cGrade);
                    }
                } 

                // store and return the transcript
                studentTranscript.Student = student;
                studentTranscript.Grades = courseGrades;

                return Request.CreateResponse(HttpStatusCode.OK, studentTranscript); 
            }
        } 
    }
}
