using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VKR_2._0.Data;
using VKR_2._0.Models;
using VKR_2._0.Models.Repository;

namespace VKR_2._0.Controllers
{
    public class PeopleController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IPersonRepository<Person> _personRepository;
        private readonly IVacancyRepository<Vacancy> _vacancyRepository;
        private readonly IEmployeeRepository<Employee> _employeeRepository;
        private readonly IInviteRepository<Invitation> _inviteRepository;

        public PeopleController(IPersonRepository<Person> personRepository, IVacancyRepository<Vacancy> vacancyRepository, 
            IEmployeeRepository<Employee> employeeRepository, IInviteRepository<Invitation> inviteRepository)
        {
            _personRepository = personRepository;
            _vacancyRepository = vacancyRepository;
            _employeeRepository = employeeRepository;
            _inviteRepository = inviteRepository;
        }

        //// GET: People
        public async Task<IActionResult> Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            // текущий работодатель
            var employee = _employeeRepository.FindById(userId);
            if (employee == null)
            {
                ViewBag.ErrorMessage = $"emploer with Id = {userId} cannot be found";
                return View("NotFound");
            }

            // все вакансии работодателя
            var vacansiesEmployee = _vacancyRepository.GetByEmployee(employee);

            // соискатели
            List<Person> Persons = (List<Person>)_personRepository.GetAllInclude();

            var model = new PeopleListModel();

            var peoplelist = new List<PeopleModel>();
            model.PeopleList = peoplelist;


            foreach (var person in Persons)
            {

                // поиск вакансий на которые не было приглашений
                bool isInvitePerson = false;
                var invitationsPerson = person.Invitations;
                foreach (var vacancyEmployee in vacansiesEmployee)
                {
                    // приглашения соискателия на вакансию 
                    var inviteList = _inviteRepository.GetPersonVacancyInvitation(person, vacancyEmployee);
                    if (inviteList.Count() == 0)
                    {
                        // можно пригласить
                        isInvitePerson = true;
                        break;
                    } else
                    {
                        // приглашать не надо
                        isInvitePerson = false;
                    }
                }

                var peopleModel = new PeopleModel();
                peopleModel.Person = person;
                peopleModel.isInvite = isInvitePerson;

                // отклики соискателя на вакансии работодателя
                var personInvitations = _inviteRepository.GetPersonInvitation(person, employee);
                foreach (var personInvitation in personInvitations)
                {
                    peopleModel.InvitationText = peopleModel.InvitationText + "Вакансия: " + personInvitation.vacancy.VacancyName + "<br>";
                    peopleModel.InvitationText = peopleModel.InvitationText + "Статус: ";
                    if (personInvitation.invitationStatus==InvitationStatus.ACCEPTED)
                    {
                        peopleModel.InvitationText = peopleModel.InvitationText + "принято <br>";
                        peopleModel.InvitationText = peopleModel.InvitationText + "С соискателем можно связаться по телефону " + person.PhoneNumber + " или электронной почте " + person.Email;
                    }
                    else if (personInvitation.invitationStatus == InvitationStatus.REJECTED)
                    {
                        peopleModel.InvitationText = peopleModel.InvitationText + "отклонено";
                    } else
                    {
                        peopleModel.InvitationText = peopleModel.InvitationText + "не рассмотрено";
                    }
                    peopleModel.InvitationText = peopleModel.InvitationText + "<br>";

                }

                peoplelist.Add(peopleModel);
            }

            var selectedItem = new List<int>();
            model.CompanyMultiSelectList = new MultiSelectList(GetCompanies(), "Id", "Title", selectedItem);
            return View(model);
        }

        private List<CompanyVM> GetCompanies()
        {
            var companies = new List<CompanyVM>();
            companies.Add(new CompanyVM() { Id = 1, Title = "C#" });
            companies.Add(new CompanyVM() { Id = 2, Title = "Базы данных" });
            companies.Add(new CompanyVM() { Id = 3, Title = ".NET Framework" });
            companies.Add(new CompanyVM() { Id = 4, Title = "Entity Framework" });
            return companies;
        }

        [HttpPost]
        public async Task<IActionResult> Index(PeopleListModel model, IEnumerable<int> Companies)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var employee = _employeeRepository.FindById(userId);
            if (employee == null)
            {
                ViewBag.ErrorMessage = $"emploer with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var vacansiesEmployee = _vacancyRepository.GetByEmployee(employee);

            List<Person> Persons = (List<Person>)_personRepository.GetAllInclude();

            var peoplelist = new List<PeopleModel>();
            model.PeopleList = peoplelist;

            foreach (var person in Persons)
            {

                // поиск вакансий на которые не было приглашений
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

                // отклики соискателя на вакансии работодателя
                var personInvitations = _inviteRepository.GetPersonInvitation(person, employee);
                foreach (var personInvitation in personInvitations)
                {
                    peopleModel.InvitationText = peopleModel.InvitationText + personInvitation.vacancy.VacancyName + personInvitation.invitationStatus;
                }

                peoplelist.Add(peopleModel);
            }

            var selectedItem = new List<int>();
            foreach (int item in Companies)
            {
                selectedItem.Add(item);
            }

            model.SelectedItemIds = Companies;
            return View(model);
        }

        public ViewResult List()
        {
            List<Person> Persons = (List<Person>)_personRepository.GetAll();
            return View(Persons);
        }

    }
}
