using AcadaAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcadaAcademy.DataModels
{
    public interface IRegistrationRepository
    {
        Registration AddEnrollment(Registration registration);
        Registration UpdateEnrollment(Registration registration);
        Registration DeleteEnrollment(int id);
        Registration fetchEnrollment(int id);
        IEnumerable<Registration> fetchAllEnrollments();
    }
}
