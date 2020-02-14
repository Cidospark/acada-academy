using AcadaAcademy.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.Models
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage ="News details cannot be more than 200 characters")]
        public string NewsDetails { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="News title cannot be more than 50 characters")]
        public string NewsTitle { get; set; }
        public string ImagePath { get; set; }

        [Required]
        public int AddedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Tag { get; set; }
        public AcadaUser User { get; set; }
    }
}
