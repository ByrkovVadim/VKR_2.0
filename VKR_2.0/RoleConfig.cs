using Microsoft.AspNetCore.Identity;
using VKR_2._0.Models;
using VKR_2._0.Models.Identity;
using VKR_2._0.Models.Repository;

namespace VKR_2._0.Data
{
    public static class RoleConfig
    {
        public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            
            string[] roleNames = { "Admin", "Applicant", "Employer" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

        }

        public static async Task CreateUsers(RoleManager<IdentityRole> roleManager, EmployeeManager userManager, IVacancyRepository<Vacancy> vacancyRepository)
        {

            Employee user1 = await userManager.FindByEmailAsync("employer@gmail.com");
            if (user1 == null)
            {
                user1 = new Employee()
                {
                    UserName = "employer@gmail.com",
                    Email = "employer@gmail.com",
                    PhoneNumber = "+79201234567",
                    EmailConfirmed = true,
                    Organisation = "Лаборатория Касперского"
                };
                await userManager.CreateAsync(user1, "123");
                await userManager.AddToRoleAsync(user1, "Employer");
            }

            Employee user2 = await userManager.FindByEmailAsync("employer2@gmail.com");
            if (user2 == null)
            {
                user2 = new Employee()
                {
                    UserName = "employer2@gmail.com",
                    Email = "employer2@gmail.com",
                    PhoneNumber = "+79201234567",
                    EmailConfirmed = true,
                    Organisation = "Яндекс"
                };
                await userManager.CreateAsync(user2, "123");
                await userManager.AddToRoleAsync(user2, "Employer");
            }


            Employee user3 = await userManager.FindByEmailAsync("employer3@gmail.com");
            if (user3 == null)
            {
                user3 = new Employee()
                {
                    UserName = "employer3@gmail.com",
                    Email = "employer3@gmail.com",
                    PhoneNumber = "+79201234567",
                    EmailConfirmed = true,
                    Organisation="Ростелеком"
                };

                Vacancy vac1 = new Vacancy();
                vac1.VacancyName = "Монтажник";
                user3.Vacancies.Add(vac1);

                Vacancy vac2 = new Vacancy();
                vac2.VacancyName = "Системный администратор";
                user3.Vacancies.Add(vac2);

                await userManager.CreateAsync(user3, "123");
                await userManager.AddToRoleAsync(user3, "Employer");
            }

            Employee user4 = await userManager.FindByEmailAsync("employer4@gmail.com");
            if (user4 == null)
            {
                user4 = new Employee()
                {
                    UserName = "employer3@gmail.com",
                    Email = "employer3@gmail.com",
                    PhoneNumber = "+79201234567",
                    EmailConfirmed = true,
                    Organisation = "SpaceX"
                };

                Vacancy vac1 = new Vacancy();
                vac1.VacancyName = "Инженер космических систем";
                user3.Vacancies.Add(vac1);

                Vacancy vac2 = new Vacancy();
                vac2.VacancyName = "Пилот Crew Dragon";
                user3.Vacancies.Add(vac2);

                await userManager.CreateAsync(user4, "123");
                await userManager.AddToRoleAsync(user4, "Employer");
            }

        }

        public static async Task CreateApplicants(RoleManager<IdentityRole> roleManager, PersonManager userManager)
        {


            Person user1 = await userManager.FindByEmailAsync("applicant1@gmail.com");
            if (user1 == null)
            {
                user1 = new Person()
                {
                    UserName = "applicant1@gmail.com",
                    Email = "applicant1@gmail.com",
                    PhoneNumber = "+79041112233",
                    EmailConfirmed = true,
                    Surname = "Гаврилов",
                    Name = "Дмитрий",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await userManager.CreateAsync(user1, "123");
                await userManager.AddToRoleAsync(user1, "Applicant");
            }

            Person user2 = await userManager.FindByEmailAsync("applicant2@gmail.com");
            if (user2 == null)
            {
                user2 = new Person()
                {
                    UserName = "applicant2@gmail.com",
                    Email = "applicant2@gmail.com",
                    PhoneNumber = "+79041112233",
                    EmailConfirmed = true,
                    Surname = "Токарева",
                    Name = "Вероника",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await userManager.CreateAsync(user2, "123");
                await userManager.AddToRoleAsync(user2, "Applicant");
            }

            Person user3 = await userManager.FindByEmailAsync("applicant3@gmail.com");
            if (user3 == null)
            {
                user3 = new Person()
                {
                    UserName = "applicant3@gmail.com",
                    Email = "applicant3@gmail.com",
                    PhoneNumber = "+79041112233",
                    EmailConfirmed = true,
                    Surname = "Зайцев",
                    Name = "Владимир",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await userManager.CreateAsync(user3, "123");
                await userManager.AddToRoleAsync(user3, "Applicant");
            }

            Person user4 = await userManager.FindByEmailAsync("applicant4@gmail.com");
            if (user4 == null)
            {
                user4 = new Person()
                {
                    UserName = "applicant4@gmail.com",
                    Email = "applicant4@gmail.com",
                    PhoneNumber = "+79041112233",
                    EmailConfirmed = true,
                    Surname = "Савельев",
                    Name = "Тимофей ",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await userManager.CreateAsync(user4, "123");
                await userManager.AddToRoleAsync(user4, "Applicant");
            }

            Person user5 = await userManager.FindByEmailAsync("applicant5@gmail.com");
            if (user5 == null)
            {
                user5 = new Person()
                {
                    UserName = "applicant5@gmail.com",
                    Email = "applicant5@gmail.com",
                    PhoneNumber = "+79041112233",
                    EmailConfirmed = true,
                    Surname = "Климова",
                    Name = "Ольга",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await userManager.CreateAsync(user5, "123");
                await userManager.AddToRoleAsync(user5, "Applicant");
            }

            Person user6 = await userManager.FindByEmailAsync("applicant6@gmail.com");
            if (user6 == null)
            {
                user6 = new Person()
                {
                    UserName = "applicant6@gmail.com",
                    Email = "applicant6@gmail.com",
                    PhoneNumber = "+79041112233",
                    EmailConfirmed = true,
                    Surname = "Афанасьев",
                    Name = "Артём",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await userManager.CreateAsync(user6, "123");
                await userManager.AddToRoleAsync(user6, "Applicant");
            }

            Person user7 = await userManager.FindByEmailAsync("applicant7@gmail.com");
            if (user7 == null)
            {
                user7 = new Person()
                {
                    UserName = "applicant73@gmail.com",
                    Email = "applicant7@gmail.com",
                    PhoneNumber = "+79041112233",
                    EmailConfirmed = true,
                    Surname = "Демина",
                    Name = "Василиса",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await userManager.CreateAsync(user7, "123");
                await userManager.AddToRoleAsync(user7, "Applicant");
            }

            Person user8 = await userManager.FindByEmailAsync("applicant8@gmail.com");
            if (user8 == null)
            {
                user8 = new Person()
                {
                    UserName = "applicant8@gmail.com",
                    Email = "applicant8@gmail.com",
                    PhoneNumber = "+79041112233",
                    EmailConfirmed = true,
                    Surname = "Фомин",
                    Name = "Матвей",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await userManager.CreateAsync(user8, "123");
                await userManager.AddToRoleAsync(user8, "Applicant");
            }

            Person user9 = await userManager.FindByEmailAsync("applicant9@gmail.com");
            if (user9 == null)
            {
                user9 = new Person()
                {
                    UserName = "applicant9@gmail.com",
                    Email = "applicant9@gmail.com",
                    PhoneNumber = "+79041112233",
                    EmailConfirmed = true,
                    Surname = "Кузьмин",
                    Name = "Родион",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await userManager.CreateAsync(user9, "123");
                await userManager.AddToRoleAsync(user9, "Applicant");
            }

            Person user10 = await userManager.FindByEmailAsync("applicant10@gmail.com");
            if (user10 == null)
            {
                user10 = new Person()
                {
                    UserName = "applicant10@gmail.com",
                    Email = "applicant10@gmail.com",
                    PhoneNumber = "+79041112233",
                    EmailConfirmed = true,
                    Surname = "Владимир",
                    Name = "Зайцев",
                    UpdateDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await userManager.CreateAsync(user10, "123");
                await userManager.AddToRoleAsync(user10, "Applicant");
            }

            
        }

        public static async Task CreateEducations(IEducationRepository<Education> educationRepository)
        {
            IEnumerable<Education> eduList = educationRepository.FindByName("Среднее");
            if (eduList.Count() == 0)
            {
                Education edu = new Education()
                {
                    EducationName = "Среднее"
                };
                educationRepository.Create(edu);
            }

            eduList = educationRepository.FindByName("Бакалавриат");
            if (eduList.Count() == 0)
            {
                Education edu = new Education()
                {
                    EducationName = "Бакалавриат"
                };
                educationRepository.Create(edu);
            }

            eduList = educationRepository.FindByName("Специалитет, магистратура");
            if (eduList.Count() == 0)
            {
                Education edu = new Education()
                {
                    EducationName = "Специалитет, магистратура"
                };
                educationRepository.Create(edu);
            }

            eduList = educationRepository.FindByName("Подготовка кадров высшей квалификации");
            if (eduList.Count() == 0)
            {
                Education edu = new Education()
                {
                    EducationName = "Подготовка кадров высшей квалификации"
                };
                educationRepository.Create(edu);
            }
        }

        public static async Task CreateSkills(ISkillRepository<Skill> skillRepository)
        {
            IEnumerable<Skill> skillList = skillRepository.FindByName("Базы данных");
            if (skillList.Count() == 0)
            {
                Skill skill = new Skill()
                {
                    SkillName = "Базы данных"
                };
                skillRepository.Create(skill);
            }

            skillList = skillRepository.FindByName("С#");
            if (skillList.Count() == 0)
            {
                Skill skill = new Skill()
                {
                    SkillName = "С#"
                };
                skillRepository.Create(skill);
            }

            skillList = skillRepository.FindByName(".NET");
            if (skillList.Count() == 0)
            {
                Skill skill = new Skill()
                {
                    SkillName = ".NET"
                };
                skillRepository.Create(skill);
            }

            skillList = skillRepository.FindByName("Entity Framework");
            if (skillList.Count() == 0)
            {
                Skill skill = new Skill()
                {
                    SkillName = "Entity Framework"
                };
                skillRepository.Create(skill);
            }
        }

        public static async Task CreateActivites(IAreaActivityRepository<AreaActivity> repository)
        {
            IEnumerable<AreaActivity> areaActivityList = repository.FindByName("Деятельность в области информации и связи");
            if (areaActivityList.Count() == 0)
            {
                AreaActivity areaActivity = new AreaActivity()
                {
                    AreaActivityName = "Деятельность в области информации и связи"
                };
                repository.Create(areaActivity);
            }

            areaActivityList = repository.FindByName("Деятельность профессиональная, научная и техническая");
            if (areaActivityList.Count() == 0)
            {
                AreaActivity areaActivity = new AreaActivity()
                {
                    AreaActivityName = "Деятельность профессиональная, научная и техническая"
                };
                repository.Create(areaActivity);
            }

           
        }


    }
}
