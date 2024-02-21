using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKR_2._0.Models
{
    [Table("Vacancy")]
    public class Vacancy
    {
        public int Id { get; set; }

        [Required]
        public Employee Employee { get; set; }

        // должность на которую нужно найти работника
        [Required]
        [MaxLength(100)]
        public string? VacancyName { get; set; }

        public Education? Education { get; set; }

        // необходимые навыки, которые указывает работодатель 
        public ICollection<SkillVacancy>? SkillVacancy { get; set; }

        // иностранные языки
        public string? ForeignLanguages { get; set; }

        // должностные обязанности
        public string? JobFunction { get; set; }

        // уровень зарплаты
        public string? Salary { get; set; }

        // иная информация
        public string? Info { get; set; }

        // true - на публикации 
        public bool isPublish { get; set; }

        public DateTime? DateCreate { get; set; }
        public DateTime? DateUpdate { get; set;}

    }
}
