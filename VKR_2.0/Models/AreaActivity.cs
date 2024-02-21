using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKR_2._0.Models
{
    [Table("AreaActivity")]
    public class AreaActivity
    {
        // сфера деятельности организации
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? AreaActivityName { get; set; }


    }
}
