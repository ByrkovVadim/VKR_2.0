

using Microsoft.EntityFrameworkCore;
using System;
using VKR_2._0.Data;
using VKR_2._0.Migrations;

namespace VKR_2._0.Models.Repository
{
    public class EFSkillRepository : ISkillRepository<Skill>
    {
        ApplicationDbContext _context;
        DbSet<Skill> _dbSet;

        public EFSkillRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Skill>();
        }

        public void Create(Skill item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public Skill FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<Skill> GetAll()
        {
            var skills = _context.Skill
                    .ToList();

            return skills;
        }

        public IEnumerable<Skill> FindByName(string name)
        {
            var skills = _context.Skill
                    .Where(u => u.SkillName == name)
                    .ToList();

            return skills;
        }

        public void Remove(Skill item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Skill item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
