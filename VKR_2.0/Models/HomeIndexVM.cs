using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace VKR_2._0.Models
{
    public class HomeIndexVM
    {
        private SelectList _CompanySelectList { get; set; }

        public SelectList CompanySelectList
        {
            get
            {
                if (_CompanySelectList == null)
                    return _CompanySelectList;

                return new SelectList(GetCompanies(), "Id", "Title");
            }

            set { _CompanySelectList = value; }
        }

        private MultiSelectList _CompanyMultiSelectList { get; set; }
        public  MultiSelectList CompanyMultiSelectList { 
            get
            {
                if (_CompanyMultiSelectList == null)
                    return _CompanyMultiSelectList;

                return new MultiSelectList(GetCompanies(), "Id", "Title");
            }
            set
            {
                _CompanyMultiSelectList = value;
            }
        }


        private List<CompanyVM> GetCompanies()
        {
            var companies = new List<CompanyVM>();
            companies.Add(new CompanyVM() { Id = 1, Title = "Company 1"});
            companies.Add(new CompanyVM() { Id = 2, Title = "Company 2"});
            companies.Add(new CompanyVM() { Id = 3, Title = "Company 3"});
            companies.Add(new CompanyVM() { Id = 4, Title = "Company 4"});
            companies.Add(new CompanyVM() { Id = 5, Title = "Company 5"});
            companies.Add(new CompanyVM() { Id = 6, Title = "Company 6"});

            return companies;
        }
    }
}
