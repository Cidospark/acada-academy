using AcadaAcademy.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.Models
{
    public class Score
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScoreId { get; set; }

        [Required]
        public int RegId { get; set; }

        [Required]
        [Range(0,30, ErrorMessage ="CA must be between 0-30")]
        public int CA { get; set; }

        [Required]
        [Range(0, 70, ErrorMessage = "Exam must be between 0-70")]
        public int Exam { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Total must be between 0-100")]
        public int Total { get; set; }

        public Grades Grade { get; set; }
        public Registration Registration { get; set; }

    }
}
