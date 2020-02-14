using AcadaAcademy.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.Models
{
    public class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }
        
        public AcadaUser User { get; set; }
        public Session Session { get; set; }
        public Course Course { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
