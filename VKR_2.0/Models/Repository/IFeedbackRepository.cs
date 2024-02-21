using Microsoft.EntityFrameworkCore;

namespace VKR_2._0.Models.Repository
{
    public interface IFeedbackRepository<Feedback> where Feedback : class
    {
        IEnumerable<Feedback> GetAll();
        Feedback FindById(int id);
        IEnumerable<Feedback> GetPersonFeedback(Person person, Vacancy vacancy);
        IEnumerable<Feedback> GetVacancyFeedback(Vacancy vacancy);
        void Remove(Feedback item);
        void Update(Feedback item);
        void Create(Feedback item);
    }
}
