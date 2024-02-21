namespace VKR_2._0.Models.Repository
{
    public interface IInviteRepository<Invitation> where Invitation : class
    {
        Invitation FindById(int id);
        IEnumerable<Invitation> GetPersonInvitation(Person person);
        IEnumerable<Invitation> GetPersonInvitation(Person person, Employee employee);
        IEnumerable<Invitation> GetPersonVacancyInvitation(Person person, Vacancy vacancy);
        void Remove(Invitation item);
        void Update(Invitation item);
        void Create(Invitation item);
    }
    
   
}
