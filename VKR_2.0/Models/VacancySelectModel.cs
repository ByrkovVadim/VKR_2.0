using Microsoft.AspNetCore.Mvc.Rendering;

namespace VKR_2._0.Models
{
    public class VacancySelectModel
    {
        public string SelectedVacancyId { get; set; }
        public string PersonId { get; set; }
        public  List<SelectListItem> VacancyList{ get; set; }
        public string PersonName { get; set; }  

    }
}
