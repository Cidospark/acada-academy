using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
