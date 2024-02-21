using Microsoft.EntityFrameworkCore;
using VKR_2._0.Data;

namespace VKR_2._0.Models.Repository
{
    public class EFPersonRepository : IPersonRepository<Person> 
    {

        ApplicationDbContext _context;
        DbSet<Person> _dbSet;

        public EFPersonRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Person>();
        }

        public void Create(Person item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public Person FindById(string id)
        {
            var persons = _context.Person
                   .Include(u => u.Education)
                   .Include(u => u.SkillPerson)
                   .ThenInclude(post => post.Skill)
                   .Where(u => u.Id == id)
                   .ToList();

            if (persons.Count > 0)
            {
                return persons.First();
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Person> GetAllInclude()
        {
            //return _dbSet.Find(id);

            var persons = _context.Person
                    .Include(u => u.Feedbacks)
                    .Include(u => u.Invitations)
                    .ThenInclude(post => post.vacancy)
                    .ToList();

            return (IEnumerable<Person>)persons;
        }

        public IEnumerable<Person> Get(Func<Person, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public IEnumerable<Person> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public void Remove(Person item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Person item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
