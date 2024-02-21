namespace VKR_2._0.Models.Repository
{
    public interface IEmployeeRepository<Employee> where Employee : class
    {
        void Create(Employee item);
        Employee FindById(string id);
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetAllInclude();
        IEnumerable<Employee> Get(Func<Employee, bool> predicate);
        void Remove(Employee item);
        void Update(Employee item);
    }
}
