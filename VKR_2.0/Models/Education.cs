using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKR_2._0.Models
{
    [Table("Education")]
    public class Education
    {
        // Уровень образования
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? EducationName { get; set; }


    }
}
