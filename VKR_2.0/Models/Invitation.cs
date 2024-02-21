using System.ComponentModel.DataAnnotations;

namespace VKR_2._0.Models
{

    public enum InvitationStatus
    {
        [Display(Name = "новый")]
        NEW,

        [Display(Name = "принят")]
        ACCEPTED,

        [Display(Name = "отклонен")]
        REJECTED
    }
    //  приглашение работодателей
    public class Invitation
    {
        public int Id { get; set; }
        [Required]
        public Person person { get; set; }
        [Required]
        public Vacancy? vacancy { get; set; }
        public DateTime? date { get; set; }

        public InvitationStatus? invitationStatus { get; set; }
    }
}
