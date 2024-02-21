namespace VKR_2._0.Models.Repository
{
    public interface IPersonRepository<Person> where Person : class
    {
        void Create(Person item);
        Person FindById(string id);
        IEnumerable<Person> GetAllInclude();
        IEnumerable<Person> GetAll();
        IEnumerable<Person> Get(Func<Person, bool> predicate);
        void Remove(Person item);
        void Update(Person item);
    }
}
