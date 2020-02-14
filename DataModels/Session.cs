using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.Models
{
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required]
        [StringLength(10,ErrorMessage ="Session must be between 4 and 10 characters", MinimumLength =4)]
        public string  Name { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
