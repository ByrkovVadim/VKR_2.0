namespace VKR_2._0.Models.Repository
{
    public interface IAreaActivityRepository<AreaActivity> where AreaActivity : class
    {
        AreaActivity FindById(int id);
        IEnumerable<AreaActivity> FindByName(string name);
        IEnumerable<AreaActivity> GetAll();
        void Remove(AreaActivity item);
        void Update(AreaActivity item);
        void Create(AreaActivity item);
    }
    
}
