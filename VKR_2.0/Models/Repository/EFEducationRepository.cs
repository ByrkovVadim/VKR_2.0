

using Microsoft.EntityFrameworkCore;
using System;
using VKR_2._0.Data;
using VKR_2._0.Migrations;

namespace VKR_2._0.Models.Repository
{
    public class EFEducationRepository : IEducationRepository<Education>
    {
        ApplicationDbContext _context;
        DbSet<Education> _dbSet;

        public EFEducationRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Education>();
        }

        public void Create(Education item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public Education FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<Education> GetAll()
        {
            var educations = _context.Education
                    .ToList();

            return educations;
        }

        public IEnumerable<Education> FindByName(string name)
        {
            var educations = _context.Education
                    .Where(u => u.EducationName == name)
                    .ToList();

            return educations;
        }

        public void Remove(Education item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Education item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
