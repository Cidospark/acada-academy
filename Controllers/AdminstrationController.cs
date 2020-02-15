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

        public AdminstrationController(
            RoleManager<IdentityRole> roleManager, 
            UserManager<AcadaUser> userManager,
            ICourseRepository courseRepository
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            this.courseRepository = courseRepository;
        }

        //List All AcadaUser
        [HttpGet]
        public IActionResult ListAllAcadaUser()
        {
            var AcadaUser = _userManager.Users;
            return View(AcadaUser);
        }


        // Edit User
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

        // POST: /Account/DeleteUser
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

        ///// <summary>
        ///// Handle Roles
        ///// </summary>
        ///// <returns></returns>
        ///
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        //// Create rows
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

        //// Edit Roles
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

        //[HttpPost]
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

        // POST: /Role
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
            return View();
        }

        // GET: Edit Course
        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            var model = courseRepository.fetchCourse(id);
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

    }
}