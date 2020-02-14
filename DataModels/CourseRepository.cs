using AcadaAcademy.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.Models
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AcadaContext context;

        public CourseRepository(AcadaContext context)
        {
            this.context = context;
        }

        public Course AddCourse(Course course)
        {
            // add course to database through context and save changes
            context.Courses.Add(course);
            context.SaveChanges();
            return course;              
        }

        public Course DeleteCourse(int id)
        {
            // delete course from database
            Course courseId = context.Courses.Find(id);
            if(courseId != null)
            {
                context.Courses.Remove(courseId);
                context.SaveChanges();
            }
            return courseId;
        }

        public IEnumerable<Course> fetchAllCourses()
        {
            return context.Courses;
        }

        public Course fetchCourse(int id)
        {
           return  context.Courses.Find(id);
            
        }

        public Course UpdateCourse(Course courseChanges)
        {
            var course = context.Courses.Attach(courseChanges);
            course.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return courseChanges;

        }
    }
}
