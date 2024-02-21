using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using VKR_2._0.Models;
using VKR_2._0.Models.Identity;
using VKR_2._0.Models.Repository;

namespace VKR_2._0.Controllers
{
    public class InviteController : Controller
    {
        private readonly IEmployeeRepository<Employee> _employeeRepository;
        private readonly IFeedbackRepository<Feedback> _feedbackRepository;
        private readonly IPersonRepository<Person> _personRepository;
        private readonly EmployeeManager _employeeManager;
        private readonly IInviteRepository<Invitation> _inviteRepository;
        private readonly IVacancyRepository<Vacancy> _vacancyRepository;
        public InviteController(IEmployeeRepository<Employee> employeeRepository,
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var person = _personRepository.FindById(userId);

            if (person == null)
            {
                ViewBag.ErrorMessage = $"person with Id = {userId} cannot be found";
                return View("NotFound");
            }

            // приглашения адресованные текущему соискателю
            var model = _inviteRepository.GetPersonInvitation(person);

            return View(model);
        }

        public async Task<IActionResult> SelectVacancy(string PersonId)
        {
            
            var person = _personRepository.FindById(PersonId);
            if (person == null)
            {
                ViewBag.ErrorMessage = $"person with Id = {PersonId} cannot be found";
                return View("NotFound");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var employee = _employeeRepository.FindById(userId);
            if (employee == null)
            {
                ViewBag.ErrorMessage = $"emploer with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var vacansies = _vacancyRepository.GetByEmployee(employee);

            var model = new VacancySelectModel();
            model.VacancyList = new List<SelectListItem>();
            model.PersonId = person.Id;
            model.PersonName = person.Name + " " + person.Surname;

            foreach (var vacancy in vacansies)
            {
                model.VacancyList.Add(new SelectListItem { Text = vacancy.VacancyName, Value = vacancy.Id.ToString() });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SelectVacancy(VacancySelectModel model, string personId)
        {
            var selectedVacancy = model.SelectedVacancyId;

            var person = _personRepository.FindById(personId);
            if (person == null)
            {
                ViewBag.ErrorMessage = $"person with Id = {personId} cannot be found";
                return View("NotFound");
            }

            var vacancy = _vacancyRepository.FindById(int.Parse(selectedVacancy));
            if (vacancy == null)
            {
                ViewBag.ErrorMessage = $"person with Id = {vacancy} cannot be found";
                return View("NotFound");
            }

            var invite = new Invitation
            {
                vacancy = vacancy,
                person = person,
                date = DateTime.Now,
            };

            _inviteRepository.Create(invite);

            List<PeopleModel> peopleList = getPeopleList();
            try
            {
                peopleList = getPeopleList();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("NotFound");
            }

            var modelView = new PeopleListModel();
            modelView.PeopleList = peopleList;

            return View("~/Views/People/Index.cshtml", modelView);
        }

        [HttpGet]
        public ActionResult PersonList()
        {

            List<PeopleModel> model;
            try {
                model = getPeopleList();
            } catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("NotFound");
            }
           

            return View("~/Views/People/Index.cshtml", model);
        }

        private List<PeopleModel>  getPeopleList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var employee = _employeeRepository.FindById(userId);
            if (employee == null)
            {
                throw new Exception($"emploer with Id = {userId} cannot be found");
            }

            var vacansiesEmployee = _vacancyRepository.GetByEmployee(employee);


            List<Person> Persons = (List<Person>)_personRepository.GetAllInclude();

            var model = new List<PeopleModel>();

            foreach (var person in Persons)
            {

                bool isInvitePerson = false;
                var invitationsPerson = person.Invitations;
                foreach (var vacancyEmployee in vacansiesEmployee)
                {
                    foreach (var invitePerson in invitationsPerson)
                    {
                        invitePerson.vacancy = vacancyEmployee;
                        isInvitePerson = true;
                        break;
                    }
                }



                var peopleModel = new PeopleModel();
                peopleModel.Person = person;
                peopleModel.isInvite = isInvitePerson;

                model.Add(peopleModel);
            }
            return model;
        }

        public async Task<IActionResult> Accept(int InviteId)
        {
            // изменить статус приглашения
            var invite = _inviteRepository.FindById(InviteId);

            if (invite == null)
            {
                ViewBag.ErrorMessage = $"invite with Id = {InviteId} cannot be found";
                return View("NotFound");
            }

            invite.invitationStatus = InvitationStatus.ACCEPTED;
            _inviteRepository.Update(invite);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reject(int InviteId)
        {
            // изменить статус приглашения
            var invite = _inviteRepository.FindById(InviteId);

            if (invite == null)
            {
                ViewBag.ErrorMessage = $"invite with Id = {InviteId} cannot be found";
                return View("NotFound");
            }

            invite.invitationStatus = InvitationStatus.REJECTED;
            _inviteRepository.Update(invite);

            return RedirectToAction(nameof(Index));

        }
    }
}
