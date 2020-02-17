using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcadaAcademy.DataModels;
using AcadaAcademy.Models;
using AcadaAcademy.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace edu_first.Controllers
{
    public class AdminstrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AcadaUser> _userManager;
        private readonly ICourseRepository courseRepository;
        private readonly ISessionRepository sessionRepository;

        //========= Constructor ==============
        public AdminstrationController(
            RoleManager<IdentityRole> roleManager, 
            UserManager<AcadaUser> userManager,
            ICourseRepository courseRepository,
            ISessionRepository sessionRepository
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            this.courseRepository = courseRepository;
            this.sessionRepository = sessionRepository;
        }

        //========= Users ==============
        // GET: List All AcadaUser
        [HttpGet]
        public IActionResult ListAllAcadaUser()
        {
            var AcadaUser = _userManager.Users;
            return View(AcadaUser);
        }
        // GET: Edit User
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return RedirectToAction("index");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles,
            };

            return View(model);
        }
        //  POST: Edit User
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return RedirectToAction("index");
            }
            else
            {
                user.LastName = model.LastName;
                user.FirstName = model.FirstName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                user.Gender = model.Gender;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {

                    return RedirectToAction("EditUser");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListAllAcadaUser");

            }
        }
        // POST: DeleteUser
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("Not Found");
            }
            else
            {

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {

                    return RedirectToAction("ListAllAcadaUser");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListAllAcadaUser");
            }

        }

        //========= Roles ==============
        // GET: Create Role
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        //// POST: Create rows
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole { Name = model.RoleName };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {

                    return RedirectToAction("CreateRole");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }
        //// GET: Edit Roles
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return RedirectToAction("index");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }
        // POST: Edit Role
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return RedirectToAction("EditRole");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {

                    return RedirectToAction("EditRole");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(model);
        }
        //// Add User to Role
        [HttpPost]
        public async Task<IActionResult> AddUserInRole(string UserId, string roleStr)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            var idResult = await _userManager.AddToRoleAsync(user, roleStr);

            if (idResult.Succeeded)
            {

                return RedirectToAction("EditRole");
            }

            foreach (var error in idResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }
        // POST: Delete Role
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("Not Found");
            }
            else
            {

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {

                    return RedirectToAction("CreateRole");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListAllAcadaUser");
            }

        }


        //========= Courses ==============
        // GET: Add Course
        [HttpGet]
        public ViewResult AddCourse()
        {
            return View();
        }

        // POST: Add Course
        [HttpPost]
        public IActionResult AddCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Course newModel = new Course
                {
                    Title = model.Title
                };
                courseRepository.AddCourse(newModel);
                return RedirectToAction("AddCourse");

            }
            return View(model);
        }
        // GET: Edit Course
        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            var course = courseRepository.fetchCourse(id);
            return View(course);
        }
        // POST: Edit Course
        [HttpPost]
        public IActionResult EditCourse(Course model)
        {
            if (ModelState.IsValid)
            {
                var course = courseRepository.fetchCourse(model.CourseId);
                course.Title = model.Title;
                courseRepository.UpdateCourse(course);
                return RedirectToAction("AddCourse");
            }
            return View(model);
        }

        // POST: Delete Course
        [HttpPost]
        public IActionResult DeleteCourse(int id)
        {
            var course = courseRepository.fetchCourse(id);

            if (course == null )
            {
                return View("NotFound");
            }

            courseRepository.DeleteCourse(id);
            return RedirectToAction("AddCourse");

        }

        //========= Sessions ==============
        // GET: Add Session
        [HttpGet]
        public IActionResult AddSession()
        {
            return View();
        }
        // POST: Add Session
        [HttpPost]
        public IActionResult AddSession(SessionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Session newModel = new Session
                {
                    Name = model.Name
                };
                sessionRepository.AddSession(newModel);
                return RedirectToAction("AddSession");
            }
            return View(model);
        }
        // GET: Edit Session
        [HttpGet]
        public IActionResult EditSession(int id)
        {
            var session = sessionRepository.fetchSession(id);
            return View(session);
        }
        // POST: Edit Session
        [HttpPost]
        public IActionResult EditSession(Session model)
        {
            if (ModelState.IsValid)
            {
                var session = sessionRepository.fetchSession(model.SessionId);
                session.Name = model.Name;
                sessionRepository.UpdateSession(session);
                return RedirectToAction("AddSession");
            }
            return View(model);
        }
        // POST: Delete Session
        [HttpPost]
        public IActionResult DeleteSession(int id)
        {
            var session = sessionRepository.fetchSession(id);

            if (session == null)
            {
                return View("NotFound");
            }

            sessionRepository.DeleteSession(id);
            return RedirectToAction("AddSession");

        }


        //========= Enrollment ==============
        // GET: Add Registration
        [HttpGet]
        public IActionResult AddEnrollment()
        {
            return View();
        }
    }
}