using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Reflection;
using System.Security.Claims;
using VKR_2._0.Migrations;
using VKR_2._0.Models;
using VKR_2._0.Models.Identity;
using VKR_2._0.Models.Repository;

namespace VKR_2._0.Controllers
{
    public class VacancyController : Controller
    {

        private readonly IVacancyRepository<Vacancy> _vacancyRepository;
        private readonly IEmployeeRepository<Employee> _employeeRepository;
        private readonly IFeedbackRepository<Feedback> _feedbackRepository;
        private readonly IPersonRepository<Person> _personRepository;
        private readonly IEducationRepository<Education> _educationRepository;
        private readonly ISkillRepository<Skill> _skillRepository;
        private readonly EmployeeManager _employeeManager;
        public VacancyController(IVacancyRepository<Vacancy> vacancyRepository, IEmployeeRepository<Employee> employeeRepository,
            IFeedbackRepository<Feedback> feedbackRepository, IPersonRepository<Person> personRepository, EmployeeManager employeeManager,
            IEducationRepository<Education> educationRepository, ISkillRepository<Skill> skillRepository)
        {
            _vacancyRepository = vacancyRepository;
            _employeeRepository = employeeRepository;
            _feedbackRepository = feedbackRepository;
            _personRepository = personRepository;
            _employeeManager = employeeManager;
            _educationRepository = educationRepository;
            _skillRepository = skillRepository;
        }

        public async Task<IActionResult> Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var person = _personRepository.FindById(userId);

            var signedEmployee = _employeeRepository.FindById(userId);
            IEnumerable<Vacancy> vacancies;
            if (signedEmployee == null)
            {
                // все вакансии для соискателя
                vacancies = _vacancyRepository.GetAll();
            } else
            {
                // только вакансии текущего работодателя
                vacancies = _vacancyRepository.GetByEmployee(signedEmployee);
            }

            //var vacancies = _vacancyRepository.GetByEmployee(signedEmployee);
            var listVacancyModel = new List<VacancyModel>();
            foreach (Vacancy vacancy in vacancies)
            {

                var feedBacks=_feedbackRepository.GetPersonFeedback(person, vacancy);

                Employee employee = vacancy.Employee;
                var thisVacancyModel = new VacancyModel
                {
                    VacancyId = vacancy.Id,
                    EmployeeName = employee?.Organisation,
                    VacancyName = vacancy.VacancyName,
                    isFeedback = feedBacks.Count() > 0,
                };

                if (thisVacancyModel.isFeedback)
                {
                    var feedBack = feedBacks.First<Feedback>();
                    thisVacancyModel.FeedbackStatus = feedBack.feedbackStatus;
                    if (thisVacancyModel.FeedbackStatus == FeedbackStatus.ACCEPTED)
                    {
                        thisVacancyModel.FeedbackText = "Вы приглашены. Можете связаться с работодателем по телефону " + employee.PhoneNumber + " или электронной почте " + employee.Email;
                    }
                }

                listVacancyModel.Add(thisVacancyModel);
            }
            return View(listVacancyModel);
        }

        public async Task<IActionResult> Feedback(int vacancyId)
        {
            var vacancy = _vacancyRepository.FindById(vacancyId);
            if (vacancy == null)
            {
                ViewBag.ErrorMessage = $"vacancy with Id = {vacancyId} cannot be found";
                return View("NotFound");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var person = _personRepository.FindById(userId);
            if (person == null)
            {
                ViewBag.ErrorMessage = $"person with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var feedBack = new Feedback
            {
                vacancy = vacancy,
                person = person,
                date = DateTime.Now,
                feedbackStatus = FeedbackStatus.NEW,
            };

            _feedbackRepository.Create(feedBack);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int vacancyId)
        {

            var vacancy = _vacancyRepository.FindById(vacancyId);

            if (vacancy == null)
            {
                ViewBag.ErrorMessage = $"vacancy with Id = {vacancyId} cannot be found";
                return View("NotFound");
            }

            return View(vacancy);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var vacancy = _vacancyRepository.FindById(id);

            if (vacancy == null)
            {
                ViewBag.ErrorMessage = $"vacancy with Id = {id} cannot be found";
                return View("NotFound");
            }

            var itemsEducation = new List<SelectListItem>();

            var educations = GetEducations();
            foreach (var education in educations)
            {
                itemsEducation.Add(new SelectListItem { Text = education.EducationName, Value = education.Id.ToString() });
            }


            ViewData["itemEducation"] = itemsEducation;

            var selectedItem = new List<int>();
            foreach (var skill in vacancy.SkillVacancy)
            {
                selectedItem.Add(skill.Id);
            }

            var itemsSkill = new MultiSelectList(GetSkills(), "Id", "SkillName", selectedItem);

            ViewData["itemSkill"] = itemsSkill;

            return View(vacancy);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("VacancyName,ForeignLanguages,JobFunction,Salary,Info,isPublish")] Vacancy vacancy, string Education, IEnumerable<int> Skills)
        {
            Vacancy vacancyNew = _vacancyRepository.FindById(id);
            vacancyNew.VacancyName = vacancy.VacancyName;
            vacancyNew.ForeignLanguages = vacancy.ForeignLanguages;
            vacancyNew.JobFunction = vacancy.JobFunction;
            vacancyNew.Salary = vacancy.Salary;
            vacancyNew.Info = vacancy.Info;
            vacancyNew.isPublish = vacancy.isPublish;

            var userId = _employeeManager.GetUserId(User);
            vacancyNew.Employee = _employeeRepository.FindById(userId);

            var edu = _educationRepository.FindById(int.Parse(Education));
            vacancyNew.Education = edu;

            var skillList = new List<SkillVacancy>();
            foreach (var skill in Skills)
            {
                Skill skillEntity = _skillRepository.FindById(skill);

                SkillVacancy skillVacancy = new SkillVacancy();
                skillVacancy.Vacancy = vacancyNew;
                skillVacancy.Skill = skillEntity;

                skillList.Add(skillVacancy);
            }
            vacancyNew.SkillVacancy = skillList;

            vacancyNew.DateUpdate = DateTime.Now;

            _vacancyRepository.Update(vacancyNew);

            return RedirectToAction(nameof(Index));
        }


        public ActionResult Create()
        {

            var itemsEducation = new List<SelectListItem>();

            var educations = GetEducations();
            foreach(var education in educations)
            {
                itemsEducation.Add(new SelectListItem { Text = education.EducationName, Value = education.Id.ToString() });
            }


            ViewData["itemEducation"] = itemsEducation;

            var selectedItem = new List<int>();

            var itemsSkill = new MultiSelectList(GetSkills(), "Id", "SkillName", selectedItem);
            
            ViewData["itemSkill"] = itemsSkill;

            Vacancy vacancy = new Vacancy();
            return View(vacancy);
        }

        private List<Education> GetEducations()
        {
            var educations = _educationRepository.GetAll();
            return (List<Education>)educations;
        }

        private List<Skill> GetSkills()
        {
            var skills = _skillRepository.GetAll();
            return (List<Skill>)skills;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Create([Bind("VacancyName,ForeignLanguages,JobFunction,Salary,Info,isPublish")] Vacancy vacancy, string Education, IEnumerable<int> Skills)
        {
            try
            {
                var userId = _employeeManager.GetUserId(User);
                vacancy.Employee = _employeeRepository.FindById(userId);

                var edu = _educationRepository.FindById(int.Parse(Education));
                vacancy.Education = edu;

                var skillList = new List<SkillVacancy>();
                foreach (var skill in Skills)
                {
                    Skill skillEntity = _skillRepository.FindById(skill);

                    SkillVacancy skillVacancy = new SkillVacancy();
                    skillVacancy.Vacancy = vacancy;
                    skillVacancy.Skill = skillEntity;

                    skillList.Add(skillVacancy);
                }
                vacancy.SkillVacancy = skillList;

                vacancy.DateCreate = DateTime.Now;
                vacancy.DateUpdate = DateTime.Now;

                _vacancyRepository.Create(vacancy);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
