

using Microsoft.EntityFrameworkCore;
using VKR_2._0.Data;
using VKR_2._0.Migrations;

namespace VKR_2._0.Models.Repository
{
    public class EFInviteRepository : IInviteRepository<Invitation>

    {
        ApplicationDbContext _context;
        DbSet<Invitation> _dbSet;

        public EFInviteRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Invitation>();
        }

        public void Create(Invitation item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public Invitation FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<Invitation> GetPersonInvitation(Person person)
        {
            var invitations = _context.Invitation
                    .Include(u => u.person)  
                    .Include(u => u.vacancy)  
                    .Include(u => u.vacancy.Employee)  
                    .Where(u => u.person == person)
                    .ToList();

            return invitations;
        }

        public IEnumerable<Invitation> GetPersonInvitation(Person person, Employee employee)
        {
            var invitations = _context.Invitation
                    .Include(u => u.person)
                    .Include(u => u.vacancy)
                    .Include(u => u.vacancy.Employee)
                    .Where(u => u.person == person && u.vacancy.Employee == employee)
                    .ToList();

            return invitations;
        }

        public IEnumerable<Invitation> GetPersonVacancyInvitation(Person person, Vacancy vacancy)
        {
            var invitations = _context.Invitation
                    .Include(u => u.person)
                    .Include(u => u.vacancy)
                    .Include(u => u.vacancy.Employee)
                    .Where(u => u.person == person && u.vacancy == vacancy)
                    .ToList();

            return invitations;
        }

        public void Remove(Invitation item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Invitation item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
