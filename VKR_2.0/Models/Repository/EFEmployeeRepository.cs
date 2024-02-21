using Microsoft.EntityFrameworkCore;
using VKR_2._0.Data;

namespace VKR_2._0.Models.Repository
{
    public class EFEmployeeRepository : IEmployeeRepository<Employee> 
    {

        ApplicationDbContext _context;
        DbSet<Employee> _dbSet;

        public EFEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Employee>();
        }

        public void Create(Employee item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public Employee FindById(string id)
        {
            var employees = _context.Employee
                   .Include(u => u.AreaActivity)
                   .Where(u => u.Id == id)
                   .ToList();
                   
           if (employees.Count>0)
            {
                return employees.First();
            } else
            {
                return null;
            }
                           
        }

        public IEnumerable<Employee> Get(Func<Employee, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<Employee> GetAllInclude()
        {
            var employees = _context.Employee
                    .Include(u => u.AreaActivity)
                    .Include(u => u.Vacancies)
                    .ToList();

            return (IEnumerable<Employee>)employees;
        }

        public void Remove(Employee item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Employee item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
