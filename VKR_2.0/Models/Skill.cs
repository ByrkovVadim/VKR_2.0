using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKR_2._0.Models
{
    [Table("Skill")]
    public class Skill
    {
        // квалификация, навык
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? SkillName { get; set; }


    }
}
