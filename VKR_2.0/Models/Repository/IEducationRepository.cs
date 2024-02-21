namespace VKR_2._0.Models.Repository
{
    public interface IEducationRepository<Education> where Education : class
    {
        Education FindById(int id);
        IEnumerable<Education> FindByName(string name);
        IEnumerable<Education> GetAll();
        void Remove(Education item);
        void Update(Education item);
        void Create(Education item);
    }
    
}
