using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VKR_2._0.Models;
using VKR_2._0.Models.Identity;
using VKR_2._0.Models.Repository;

namespace VKR_2._0.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeManager _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmployeeRepository<Employee> _employeeRepository;

        public EmployeeController(EmployeeManager userManager, RoleManager<IdentityRole> roleManager, IEmployeeRepository<Employee> employeeRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
        }
        public async Task<IActionResult> Index()
        {
            var users = _employeeRepository.GetAllInclude();
            var employeeModelList = new List<EmployeeModel>();
            foreach (Employee user in users)
            {
                var thisViewModel = new EmployeeModel();
                thisViewModel.UserId = user.Id;
                if (user.AreaActivity != null)
                {
                    thisViewModel.Area = user.AreaActivity.AreaActivityName;
                } else
                {
                    thisViewModel.Area = "";
                }
                thisViewModel.numVacancy = user.Vacancies.Count();
                thisViewModel.Organisation = user.Organisation;
                thisViewModel.UserName = user.UserName;
                
                employeeModelList.Add(thisViewModel);
            }
            return View(employeeModelList);
        }
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<EmployeeModel>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<EmployeeModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}
