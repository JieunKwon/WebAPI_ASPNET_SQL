using StudentGradeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentGradeAPI.Controllers
{
    /// <summary>
    /// -- Challenge 4 --
    /// Add an endpoint to insert a student grade 
    /// </summary> 
    public class GradesController : ApiController
    {
        private SchoolDBContext dbContext = new SchoolDBContext();

        // POST: api/Grades/
        // studentGrade {"StudentID":int,"CourseID":int,"Grade":nullable decimal}
        public HttpResponseMessage PostGrades(StudentGrade studentGrade)
        {
            try
            {
                // model validation
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                // check a valid CourseID 
                if (!CourseValid(studentGrade.CourseID))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Course with ID " + studentGrade.CourseID.ToString() + " is not valid");
                } 

                // check a valid StudentID 
                if (!StudentValid(studentGrade.StudentID))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "ID " + studentGrade.StudentID.ToString() + " is not valid for a student");
                }
                      
                // check a grade if exists 
                if(StudentGradeExists(studentGrade.CourseID, studentGrade.StudentID))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Grade already exists for student with ID " + studentGrade.StudentID.ToString() + 
                        " and course with ID " + studentGrade.CourseID.ToString());
                }

                // check a valid Grade for null or between 0.00 and 4.00 
                if (studentGrade.Grade != null && (studentGrade.Grade < 0.00m || studentGrade.Grade > 4.00m))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Grade must be null or a numeric value between 0.00 and 4.00");
                }
                     
                // update data
                dbContext.StudentGrades.Add(studentGrade);
                dbContext.SaveChanges();
                    
                // return a response with new id
                return Request.CreateResponse(HttpStatusCode.Created, 
                    new {   gradeId = studentGrade.EnrollmentID,
                            studentId = studentGrade.StudentID,
                            courseId = studentGrade.CourseID,
                            grade = studentGrade.Grade
                    });   
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Check the existence of data (return true if grade exists)  
        private bool StudentGradeExists(int courseId, int studentId)
        {
            return dbContext.StudentGrades.Count(e => e.CourseID == courseId
                                              && e.StudentID == studentId) > 0; 
        }

        // Check the valid course (return true if course exists) 
        private bool CourseValid(int courseId)
        {
            return dbContext.Courses.FirstOrDefault(e => e.CourseID == courseId) != null;
        }

        // Check the valid student (return true if personId exists and the ID is for a student)
        private bool StudentValid(int studentId)
        { 
            return dbContext.People.FirstOrDefault(e => e.PersonID == studentId 
                                                && e.Discriminator == "Student") != null; 
        }
    }
}
