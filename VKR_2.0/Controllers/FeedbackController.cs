using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using VKR_2._0.Models;
using VKR_2._0.Models.Identity;
using VKR_2._0.Models.Repository;

namespace VKR_2._0.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IEmployeeRepository<Employee> _employeeRepository;
        private readonly IFeedbackRepository<Feedback> _feedbackRepository;
        private readonly IPersonRepository<Person> _personRepository;
        private readonly EmployeeManager _employeeManager;
        private readonly IInviteRepository<Invitation> _inviteRepository;
        private readonly IVacancyRepository<Vacancy> _vacancyRepository;
        public FeedbackController(IEmployeeRepository<Employee> employeeRepository,
            IFeedbackRepository<Feedback> feedbackRepository, IPersonRepository<Person> personRepository, EmployeeManager employeeManager,
            IInviteRepository<Invitation> inviteRepository, IVacancyRepository<Vacancy> vacancyRepository)
        {
            _employeeRepository = employeeRepository;
            _feedbackRepository = feedbackRepository;
            _personRepository = personRepository;
            _employeeManager = employeeManager;
            _inviteRepository = inviteRepository;
            _vacancyRepository = vacancyRepository;
        }

        public IActionResult Index()
        {
            var model = new List<Feedback>();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var employee = _employeeRepository.FindById(userId);

            // все вакансии текущего работодателя
            var vacancies = _vacancyRepository.GetByEmployee(employee);

            foreach(var vacancy in vacancies)
            {
                // отклики по вакансии
                var feedbackVacancyList = _feedbackRepository.GetVacancyFeedback(vacancy);
                model.AddRange(feedbackVacancyList);
            }
            
            return View(model);
        }

        public async Task<IActionResult> Accept(int FeedbackId)
        {
            // изменить статус
            var feedback = _feedbackRepository.FindById(FeedbackId);

            if (feedback == null)
            {
                ViewBag.ErrorMessage = $"feedback with Id = {FeedbackId} cannot be found";
                return View("NotFound");
            }

            feedback.feedbackStatus = FeedbackStatus.ACCEPTED;
            _feedbackRepository.Update(feedback);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reject(int FeedbackId)
        {
            // изменить статус
            var feedback = _feedbackRepository.FindById(FeedbackId);

            if (feedback == null)
            {
                ViewBag.ErrorMessage = $"feedback with Id = {FeedbackId} cannot be found";
                return View("NotFound");
            }

            feedback.feedbackStatus = FeedbackStatus.REJECTED;
            _feedbackRepository.Update(feedback);

            return RedirectToAction(nameof(Index));

        }
    }
}
