using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcadaAcademy.Models;

namespace AcadaAcademy.DataModels
{
    public class RegistrationRepository : IRegistrationRepository 
    {
        private readonly AcadaContext acadaContext;

        public RegistrationRepository(AcadaContext acadaContext)
        {
            this.acadaContext = acadaContext;
        }
        public Registration AddEnrollment(Registration registration)
        {
            acadaContext.Registrations.Add(registration);
            acadaContext.SaveChanges();
            return registration;
        }

        public Registration DeleteEnrollment(int id)
        {
            var registration = acadaContext.Registrations.Find(id);
            if(registration != null)
            {
                acadaContext.Remove(registration);
                acadaContext.SaveChanges();
            }
            return registration;
        }

        public IEnumerable<Registration> fetchAllEnrollments()
        {
            return acadaContext.Registrations;
        }

        public Registration fetchEnrollment(int id)
        {
            return acadaContext.Registrations.Find(id);
        }

        public Registration UpdateEnrollment(Registration regChanges)
        {
            var reg = acadaContext.Registrations.Attach(regChanges);
            reg.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            acadaContext.SaveChanges();
            return regChanges;
        }
    }
}
