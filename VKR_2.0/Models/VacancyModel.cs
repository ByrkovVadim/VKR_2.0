namespace VKR_2._0.Models
{
    public class VacancyModel
    {

        public int VacancyId { get; set; }
        public string EmployeeName { get; set; }
        public string VacancyName { get; set; }

        public bool isFeedback { get; set; } // есть ли отклик на вакансию

        public FeedbackStatus? FeedbackStatus { get; set; } // статус отклика
        public string? FeedbackText { get; set; } // текст приглашения (если есть)

    }
}
