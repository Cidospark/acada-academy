using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AcadaAcademy.DataModels
{
    public class AcadaUser : IdentityUser
    {
        public DateTime LastLogin { get; set; }
    }
}
