using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.Models
{
    public interface ICourseRepository
    {
        Course AddCourse(Course course);
        Course UpdateCourse(Course courseChanges);
        Course DeleteCourse(int id);
        Course fetchCourse(int id);
        IEnumerable<Course> fetchAllCourses(); 
    }
}
