using Microsoft.AspNetCore.Mvc.Rendering;

namespace VKR_2._0.Models
{
    public class PeopleModel
    {

        public Person Person { get; set; }
        public List<Invitation> invitations { get; set; }
        public List<Feedback> feedbacks { get; set; }

        // есть ли вакансии на которые еще не отправлялось приглащзение
        public bool isInvite { get; set; }
        public InvitationStatus? InvitationStatus { get; set; } // статус приглашения
        public string? InvitationText { get; set; } // текст приглашения (если есть)


    }
}
