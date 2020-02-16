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
        public int SessionId { get; set; }

        public string  Name { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
