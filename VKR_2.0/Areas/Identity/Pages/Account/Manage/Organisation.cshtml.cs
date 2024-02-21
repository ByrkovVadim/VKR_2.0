// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using VKR_2._0.Migrations;
using VKR_2._0.Models;
using VKR_2._0.Models.Identity;
using VKR_2._0.Models.Repository;

namespace VKR_2._0.Areas.Identity.Pages.Account.Manage
{
    public class OrganisationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly EmployeeManager _employeeManager;
        private readonly IEmployeeRepository<Employee> _employeeRepository;
        private readonly IAreaActivityRepository<AreaActivity> _areaActivityRepository;

        public OrganisationModel(
            UserManager<IdentityUser> userManager,
            EmployeeManager employeeManager,
            IEmployeeRepository<Employee> employeeRepository,
            SignInManager<IdentityUser> signInManager,
            IAreaActivityRepository<AreaActivity> areaActivityRepository,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _employeeManager = employeeManager;
            _employeeRepository = employeeRepository;
            _areaActivityRepository = areaActivityRepository;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Organisation { get; set; }
        public string Adress { get; set; }
        public string ContactPerson { get; set; }
        public AreaActivity AreaActivity { get; set; }
        public int SelectedOrderId { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        //public bool IsEmailConfirmed { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        //[BindProperty]
        //public InputModel Input { get; set; }

        ///// <summary>
        /////     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /////     directly from your code. This API may change or be removed in future releases.
        ///// </summary>
        //public class InputModel
        //{
        //    /// <summary>
        //    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        //    ///     directly from your code. This API may change or be removed in future releases.
        //    /// </summary>
        //    [Required]
        //    [EmailAddress]
        //    [Display(Name = "New email")]
        //    public string NewEmail { get; set; }
        //}

        private async Task LoadAsync(IdentityUser user)
        {
            //var email = await _userManager.GetEmailAsync(user);
            //Email = email;

            //Input = new InputModel
            //{
            //    NewEmail = email,
            //};

            //IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            //var employee = await _employeeManager.FindByEmailAsync(user.Email);

            var employee = _employeeRepository.FindById(user.Id);

            Organisation = employee.Organisation;
            Adress = employee.Adress;
            ContactPerson = employee.ContactPerson;
            AreaActivity = employee.AreaActivity;

            var areas = _areaActivityRepository.GetAll();
            if (AreaActivity != null)
            {
                SelectedOrderId = AreaActivity.Id;
                SelectList selectListItems = new SelectList(areas, "Id", "AreaActivityName", AreaActivity.Id);
                ViewData["itemAreaActivity"] = selectListItems;
            } else
            {
                SelectList selectListItems = new SelectList(areas, "Id", "AreaActivityName");
                ViewData["itemAreaActivity"] = selectListItems;
            }

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeOrganisationAsync(string Organisation, string Adress, string ContactPerson, int SelectedOrderId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);

            var employee = _employeeRepository.FindById(userId);
            if (employee == null)
            {
                return NotFound($"Unable to load employee with ID '{userId}'.");
            }

            employee.Organisation = Organisation;
            employee.Adress = Adress;
            employee.ContactPerson = ContactPerson;

            var area = _areaActivityRepository.FindById(SelectedOrderId);
            employee.AreaActivity = area;
            
            _employeeRepository.Update(employee);
            
            StatusMessage = "Данные организации изменены.";
            return RedirectToPage();
        }

        
    }
}
