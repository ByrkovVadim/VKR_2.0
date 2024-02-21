using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKR_2._0.Models
{
    //public enum Education
    //{
    //    [Display(Name = "среднее общее образование")]
    //    EDU_01,

    //    [Display(Name = "неполное высшее")]
    //    EDU_02,

    //    [Display(Name = "высшее")]
    //    EDU_03,

    //    [Display(Name = "бакалавриат")]
    //    EDU_04,

    //    [Display(Name = "магистратура")]
    //    EDU_05
    //}

    public enum Gender
    {
        [Display(Name = "мужской")]
        MALE,

        [Display(Name = "женский")]
        FEMALE
    }

    [Table("Person")]
    public class Person : IdentityUser
    {

        //public int Id { get; set; }
        // Column("Имя"),
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        // Column("Фамилия"), 
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        //Column("Отчество"),
        [MaxLength(50)]
        public string? Patronymic { get; set; }

        //[Column("Электронная почта")]
        //public string email { get; set; }

        //[Column("Номер телефона")]
        //public string? phone { get; set; }

        //[Column("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? Date_of_birth { get; set; }

        //[Column("Пол")]
        public Gender? Gender { get; set; }

        //[Column("Должность")]
        public string? Post { get; set; }

        //[Column("Образование")]
        public Education? Education { get; set; }

        // навыки, которыми владеет соискатель
        public ICollection<SkillPerson>? SkillPerson { get; set; }

        //[Column("Желательная заработная плата")]
        public uint Expected_salary { get; set; }

        //[Column("Иная информация")]
        public string? Other_information { get; set; }

        //[Column("Дата заполнения")]
        public DateTime? CreateDate { get; set; }

        //[Column("Дата изменения")]
        public DateTime? UpdateDate { get; set; }

        public ICollection<Invitation>? Invitations { get; }
        public ICollection<Feedback>? Feedbacks { get; }

    }
}
