using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace VKR_2._0.Models.Identity
{
    public class PersonManager : UserManager<Person>
    {
        public PersonManager(IUserStore<Person> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Person> passwordHasher, IEnumerable<IUserValidator<Person>> userValidators, IEnumerable<IPasswordValidator<Person>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Person>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
       
    }
}
