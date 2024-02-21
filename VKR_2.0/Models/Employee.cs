using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace VKR_2._0.Models
{
    
    [Table("Employee")]
    public class Employee : IdentityUser
    {

        //[Required]
        //[MaxLength(50)]
        // название организации
        public string? Organisation { get; set; }

        // сфера деятельности
        public AreaActivity? AreaActivity { get; set; }

        public string? Adress { get; set; }
        // контактное лицо
        public string? ContactPerson { get; set; }

        public ICollection<Vacancy> Vacancies { get; } = new List<Vacancy>();

    }
}
