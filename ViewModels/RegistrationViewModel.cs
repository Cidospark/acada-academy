using AcadaAcademy.DataModels;
using AcadaAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.ViewModels
{
    public class RegistrationViewModel
    {
        public string RegistrationId { get; set; }
        public AcadaUser User { get; set; }
        public Session Session { get; set; }
        public Course Course { get; set; }
    }
}
