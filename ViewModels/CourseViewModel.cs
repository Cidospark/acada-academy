using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.ViewModels
{
    public class CourseViewModel
    {
        [Display(Name = "Course Title")]
        [Required]
        [MaxLength(50, ErrorMessage = "Course title cannot be more than 50 characters")]
        public string Title { get; set; }
    }
}
