using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKR_2._0.Models
{
    [Table("SkillVacancy")]
    public class SkillVacancy
    {
        // навыки указанные работодателем в вакансии
        public int Id { get; set; }

        public Skill Skill { get; set; }
        public Vacancy Vacancy { get; set; }


    }
}
