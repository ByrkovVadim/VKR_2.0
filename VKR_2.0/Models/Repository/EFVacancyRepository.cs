using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VKR_2._0.Data;
using VKR_2._0.Migrations;

namespace VKR_2._0.Models.Repository
{
    public class EFVacancyRepository : IVacancyRepository<Vacancy>
    {

        ApplicationDbContext _context;
        DbSet<Vacancy> _dbSet;

        public EFVacancyRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Vacancy>();
        }

        public IEnumerable<Vacancy> GetAll()
        {
            var vacancies = _context.Vacancy
                    .Include(u => u.Employee)  // подгружаем данные по компаниям
                    .ToList();

            return vacancies;
        }

        public Vacancy FindById(int id)
        {
            //return _dbSet.Find(id);
            var vacancy = _context.Vacancy
                   .Include(u => u.Employee)
                   .Include(u => u.Education)
                   .Include(u => u.SkillVacancy)
                   .ThenInclude(post => post.Skill)
                   .Where(u => u.Id == id)
                   .ToList().First();

            return vacancy;
        }

        public void Remove(Vacancy item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Vacancy item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Create(Vacancy item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public IEnumerable<Vacancy> GetByEmployee(Employee employee)
        {
            
            var vacancies = _context.Vacancy
                   .Include(u => u.Employee)  
                   .Where(u => u.Employee == employee)
                   .ToList();

            return vacancies;
        }
    }
}
