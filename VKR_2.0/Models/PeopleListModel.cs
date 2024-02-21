using Microsoft.AspNetCore.Mvc.Rendering;

namespace VKR_2._0.Models
{

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PeopleListModel
    {
        public PeopleListModel()
        {
        }

        public PeopleListModel(List<PeopleModel> peopleList, MultiSelectList companyMultiSelectList)
        {
            PeopleList = peopleList;
            CompanyMultiSelectList = companyMultiSelectList;
        }

        public List<PeopleModel> PeopleList { get; set; }

        public MultiSelectList CompanyMultiSelectList { get; set; }

        public IEnumerable<int> SelectedItemIds { get; set; }


        public IEnumerable<Item> AvailableItems
        {
            get
            {
                return new[]
                {
                new Item { Id = 1, Name = "Базы данных" },
                new Item { Id = 2, Name = "C#" },
                new Item { Id = 3, Name = ".NET" },
                new Item { Id = 4, Name = "EntityFramework" },
            };
            }
        }

        public IEnumerable<Item> EducationItems
        {
            get
            {
                return new[]
                {
                new Item { Id = 1, Name = "Среднее" },
                new Item { Id = 2, Name = "Бакалавриат" },
                new Item { Id = 3, Name = "Специалитет, магистратура" },
                new Item { Id = 4, Name = "Подготовка кадров высшей квалификации" },
            };
            }
        }


    }
}
