using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using StudentGradeAPI.Models;

namespace StudentGradeAPI.Controllers
{
    /// <summary> 
    /// -- Challenge 3 --
    /// Modify the StudentGrade table to enforce the following rules:
    /// 1.If not NULL, Grade must be a value between 0.00 and 4.00 inclusive 
    /// 2.The combination of CourseID and StudentID must be unique 
    /// </summary> 
    public class StudentGradesController : ApiController
    {
        private SchoolDBContext db = new SchoolDBContext();

        // GET: api/StudentGrades
        public IHttpActionResult GetValidStudentGrades()
        {
            string resultMsg = "";      // for return result

            // 
            // 1. Delete rows the duplicated information with CourseID & StudentID 
            // 
            var studentGrade = db.StudentGrades.OrderBy(c => c.StudentID).OrderBy(c => c.CourseID);
             
            // remove all duplicated rows except the first row
            int preCourseID = 0;
            int preStudentID = 0;
            int count = 0;
            foreach (var g in studentGrade)
            { 
                if(preCourseID == g.CourseID && preStudentID == g.StudentID) {
                    db.StudentGrades.Remove(g);
                    count++;
                }
                preCourseID = g.CourseID;
                preStudentID = g.StudentID;
            }
  
            try
            {
                db.SaveChanges(); 
                resultMsg += count.ToString() + " row(s) deleted "; 
            }
            catch (DbUpdateConcurrencyException)
            {
                resultMsg += " 0 row deleted ";
            }

            //
            // 2. Modify grade rows with range between 0.00 and 4.00 
            //
            // set for value range
            var minVal = 0.00M;
            var maxVal = 4.00M;
            count = 0;

            // studentGrades filtering for searching the invalid grade
            IQueryable<StudentGrade> studentGrades = 
                db.StudentGrades.Where(x => x.Grade != null && 
                                        (x.Grade < minVal || x.Grade > maxVal));
            
            // update data
            foreach (var g in studentGrades)
            {
                // if grade < 0.00, force to edit 0.00 
                if (g.Grade < minVal) g.Grade = minVal;
                // if grade > 4.00, force to edit 4.00
                else g.Grade = maxVal;

                count++;
            }
             
            try
            {
                db.SaveChanges();
                resultMsg += count.ToString() + " row(s) modified";
            }
            catch (DbUpdateConcurrencyException)
            {
                resultMsg += " 0 row modified";
            }
                 
            return Ok(resultMsg);
        } 
    }
}