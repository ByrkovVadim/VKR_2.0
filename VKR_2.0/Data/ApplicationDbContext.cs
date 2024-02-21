using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VKR_2._0.Models;

namespace VKR_2._0.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(@"host=localhost;port=5433;database=db;username=postgres;password=postgres");
            // читаем строку подключения из appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine);
        }

        public DbSet<Person>? Person { get; set; }
        public DbSet<Employee>? Employee { get; set; }
        public DbSet<Vacancy>? Vacancy { get; set; }
        public DbSet<Feedback>? Feedback { get; set; }
        public DbSet<Invitation>? Invitation { get; set; }
        public DbSet<Education>? Education { get; set; }
        public DbSet<Skill>? Skill { get; set; }
        public DbSet<AreaActivity>? AreaActivity { get; set; }


    }
}