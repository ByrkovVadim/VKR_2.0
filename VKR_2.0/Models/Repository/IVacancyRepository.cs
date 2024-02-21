using Microsoft.EntityFrameworkCore;

namespace VKR_2._0.Models.Repository
{
    public interface IVacancyRepository<Vacancy> where Vacancy : class
    {
        IEnumerable<Vacancy> GetAll();
        IEnumerable<Vacancy> GetByEmployee(Employee employee);
        Vacancy FindById(int id);
        void Remove(Vacancy item);
        void Update(Vacancy item);

        void Create(Vacancy item);
    }
}
