

using Microsoft.EntityFrameworkCore;
using System;
using VKR_2._0.Data;
using VKR_2._0.Migrations;

namespace VKR_2._0.Models.Repository
{
    public class EFAreaActivityRepository : IAreaActivityRepository<AreaActivity>
    {
        ApplicationDbContext _context;
        DbSet<AreaActivity> _dbSet;

        public EFAreaActivityRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<AreaActivity>();
        }

        public void Create(AreaActivity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public AreaActivity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<AreaActivity> GetAll()
        {
            var activites = _context.AreaActivity
                    .ToList();

            return activites;
        }

        public IEnumerable<AreaActivity> FindByName(string name)
        {
            var activites = _context.AreaActivity
                    .Where(u => u.AreaActivityName == name)
                    .ToList();

            return activites;
        }

        public void Remove(AreaActivity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Update(AreaActivity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
