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
        [StringLength(10, ErrorMessage = "Invalid Session Fromat. See example: 2020/2021", MinimumLength = 4)]
        [RegularExpression(@"^[0-9]{4}/[0-9]{4}$", ErrorMessage = "Invalid Session Fromat")]
        public string  Name { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
