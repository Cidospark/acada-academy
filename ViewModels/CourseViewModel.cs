using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.ViewModels
{
    public class CourseViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Display(Name = "Course Title")]
        [Required]
        [MaxLength(50, ErrorMessage = "Course title cannot be more than 50 characters")]
        public string Title { get; set; }
    }
}
