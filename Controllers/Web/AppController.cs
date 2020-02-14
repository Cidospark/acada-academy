using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcadaAcademy.ViewModels;
using AcadaAcademy.Services;
using Microsoft.Extensions.Configuration;
using AcadaAcademy.DataModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AcadaAcademy.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _imailservice;
        private IConfiguration _config;
        private IAcadaRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailservice, IConfiguration config, IAcadaRepository repository, ILogger<AppController> logger)
        {
            _imailservice = mailservice;
            _config = config;
            _repository = repository;
            _logger = logger;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

       
        // GET: /<controller>/
        public IActionResult About()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult Contact()
        {
  
            return View();
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "We dont support AOL Addresses");
            }

            if (ModelState.IsValid)
            {

                _imailservice.sendMail(_config["MailSettings:ToAddress"], model.Email, "From Lost Zone", model.Message);

                ModelState.Clear();

                ViewBag.UserMessage = "Message Successfully Sent";
                
            }
            return View();
        }
    
    }
}
