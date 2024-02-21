using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKR_2._0.Models
{
    [Table("SkillPerson")]
    public class SkillPerson
    {
        // навыки указанные в анкете соискателя
        public int Id { get; set; }

        public Skill Skill { get; set; }
        public Person Person { get; set; }


    }
}
