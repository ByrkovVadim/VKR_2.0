using System.ComponentModel.DataAnnotations;

namespace VKR_2._0.Models
{

    public enum FeedbackStatus
    {
        [Display(Name = "новый")]
        NEW,

        [Display(Name = "принят")]
        ACCEPTED,

        [Display(Name = "отклонен")]
        REJECTED
    }

    // отклики соискателей на вакансии
    public class Feedback
    {

        public int Id { get; set; }

        [Required]
        public Person person { get; set; }
        [Required]
        public Vacancy vacancy { get; set; }
        public DateTime? date { get; set; }

        public FeedbackStatus? feedbackStatus { get; set; }


    }
}
