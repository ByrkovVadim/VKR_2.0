using Microsoft.EntityFrameworkCore;
using System.Linq;
using VKR_2._0.Data;
using VKR_2._0.Migrations;

namespace VKR_2._0.Models.Repository
{
    public class EFFeedbackRepository : IFeedbackRepository<Feedback>
    {

        ApplicationDbContext _context;
        DbSet<Feedback> _dbSet;

        public EFFeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Feedback>();
        }

        public IEnumerable<Feedback> GetAll()
        {
            
            var vacancies = _context.Feedback
                    .Include(u => u.person)  
                    .Include(u => u.vacancy)  
                    .ToList();

            return vacancies;
        }

        public Feedback FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(Feedback item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Feedback item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Create(Feedback item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public IEnumerable<Feedback> GetPersonFeedback(Person person, Vacancy vacancy)
        {
            var vacancies = _context.Feedback
                    .Include(u => u.person)  
                    .Include(u => u.vacancy) 
                    .Where(u => u.person == person && u.vacancy == vacancy)
                    .ToList();

            return vacancies;
        }

        public IEnumerable<Feedback> GetVacancyFeedback(Vacancy vacancy)
        {
            var vacancies = _context.Feedback
                    .Include(u => u.person)  
                    .Include(u => u.vacancy) 
                    .Where(u => u.vacancy == vacancy)
                    .ToList();

            return vacancies;
        }
    }
}
