// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VKR_2._0.Migrations;
using VKR_2._0.Models;
using VKR_2._0.Models.Identity;
using VKR_2._0.Models.Repository;

namespace VKR_2._0.Areas.Identity.Pages.Account.Manage
{
    public class AnketaModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly PersonManager _personManager;
        private readonly IPersonRepository<Person> _personRepository;
        private readonly IEducationRepository<Education> _educationRepository;
        private readonly ISkillRepository<Skill> _skillRepository;

        public AnketaModel(
            UserManager<IdentityUser> userManager,
            PersonManager personManager,
            IPersonRepository<Person> personRepository,
            SignInManager<IdentityUser> signInManager,
            IEducationRepository<Education> educationRepository,
            ISkillRepository<Skill> skillRepository,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _personManager = personManager;
            _personRepository = personRepository;
            _educationRepository = educationRepository;
            _skillRepository = skillRepository;
        }


        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date_of_birth { get; set; }
        public Gender? Gender { get; set; }

        public string Post { get; set; }
        public Education Education { get; set; }

        public ICollection<SkillPerson> SkillPerson { get; }

        public uint Expected_salary { get; set; }

        public string Other_information { get; set; }

        public int EducationOrderId { get; set; }

        const string MALE = "мужской";
        const string FEMALE = "женский";

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(IdentityUser user)
        {
            var person = _personRepository.FindById(user.Id);

            Name = person.Name;
            Surname = person.Surname;
            Patronymic = person.Patronymic;
            Date_of_birth = person.Date_of_birth;
            Gender = person.Gender;
            Post = person.Post;
            Education = person.Education;

            ViewData["itemGender"] = GetGenderSelectList(Gender);

            Expected_salary = person.Expected_salary;
            Other_information = person.Other_information;


            var educations = _educationRepository.GetAll();
            if (person.Education != null)
            {
                EducationOrderId = Education.Id;
                SelectList selectListItems = new SelectList(educations, "Id", "EducationName", EducationOrderId);
                ViewData["itemEducation"] = selectListItems;
            }
            else
            {
                SelectList selectListItems = new SelectList(educations, "Id", "EducationName");
                ViewData["itemEducation"] = selectListItems;
            }

            var selectedItem = new List<int>();
            foreach (var skill in person.SkillPerson)
            {
                selectedItem.Add(skill.Skill.Id);
            }

            var skills = _skillRepository.GetAll();
            var itemsSkill = new MultiSelectList(skills, "Id", "SkillName", selectedItem);

            ViewData["itemSkill"] = itemsSkill;

        }

        public static SelectList GetGenderSelectList(Gender? selectedGender)
        {

            List<string> genderList = new List<string>();
            genderList.Add(MALE);
            genderList.Add(FEMALE);

            string selectedItem="";
            if (selectedGender.Equals(Models.Gender.MALE))
            {
                selectedItem = MALE;
            }
            else if (selectedGender.Equals(Models.Gender.FEMALE))
            {
                selectedItem = FEMALE;
            }

            return new SelectList(genderList, selectedItem);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeAnketaAsync(string Surname, string Name, string Patronymic, string Post, string Gender, DateTime Date_of_birth, uint Expected_salary, string Other_information, int EducationOrderId, IEnumerable<int> Skills)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);

            var person = _personRepository.FindById(userId);
            if (person == null)
            {
                return NotFound($"Unable to load person with ID '{userId}'.");
            }

            person.Surname = Surname;
            person.Name = Name;
            person.Patronymic = Patronymic;
            person.Post = Post;
            if (MALE.Equals(Gender))
            {
                person.Gender = Models.Gender.MALE;
            }
            else if (FEMALE.Equals(Gender))
            {
                person.Gender = Models.Gender.FEMALE;
            }

            person.Date_of_birth = Date_of_birth;
            person.Expected_salary = Expected_salary;
            person.Other_information = Other_information;

            var education = _educationRepository.FindById(EducationOrderId);
            person.Education = education;

            var skillList = new List<SkillPerson>();
            foreach (var skill in Skills)
            {
                Skill skillEntity = _skillRepository.FindById(skill);

                SkillPerson skillPerson = new SkillPerson();
                skillPerson.Person = person;
                skillPerson.Skill = skillEntity;

                skillList.Add(skillPerson);
            }
            person.SkillPerson = skillList;

            _personRepository.Update(person);

            StatusMessage = "Данные изменены.";
            return RedirectToPage();
        }

        
    }
}
