using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadaAcademy.Services
{
    public interface IMailService
    {
        void sendMail(string to, string from, string subject, string body);

    }
}
