using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using VKR_2._0.Models;

namespace VKR_2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var model = new HomeIndexVM();

            ViewBag.CompanyId = new SelectList(GetCompanies(), "Id", "Title");

            return View(model);
        }

        private List<CompanyVM> GetCompanies()
        {
            var companies = new List<CompanyVM>();
            companies.Add(new CompanyVM() { Id = 1, Title = "Company 1" });
            companies.Add(new CompanyVM() { Id = 2, Title = "Company 2" });
            companies.Add(new CompanyVM() { Id = 3, Title = "Company 3" });
            companies.Add(new CompanyVM() { Id = 4, Title = "Company 4" });
            companies.Add(new CompanyVM() { Id = 5, Title = "Company 5" });
            companies.Add(new CompanyVM() { Id = 6, Title = "Company 6" });

            return companies;
        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}