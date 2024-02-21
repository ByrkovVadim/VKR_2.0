using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace VKR_2._0.Models.Identity
{
    public class EmployeeManager : UserManager<Employee>
    {
        public EmployeeManager(IUserStore<Employee> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Employee> passwordHasher, IEnumerable<IUserValidator<Employee>> userValidators, IEnumerable<IPasswordValidator<Employee>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Employee>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

    }
}
