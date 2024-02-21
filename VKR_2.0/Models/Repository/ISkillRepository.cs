namespace VKR_2._0.Models.Repository
{
    public interface ISkillRepository<Skill> where Skill : class
    {
        Skill FindById(int id);
        IEnumerable<Skill> FindByName(string name);
        IEnumerable<Skill> GetAll();
        void Remove(Skill item);
        void Update(Skill item);
        void Create(Skill item);
    }
    

    
}
