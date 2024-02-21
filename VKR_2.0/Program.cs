using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using VKR_2._0.Data;
using VKR_2._0.Models;
using VKR_2._0.Models.Identity;
using VKR_2._0.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddUserManager<UserManager<IdentityUser>>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityCore<Employee>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddUserManager<EmployeeManager>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityCore<Person>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddUserManager<PersonManager>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 0;
});

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
//{
//    config.Password.RequireNonAlphanumeric = false; //optional
//    config.SignIn.RequireConfirmedEmail = false; //optional
//})
//    .AddRoles<IdentityRole>()
//    .AddRoleManager<RoleManager<IdentityRole>>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
//{
//    config.Password.RequireNonAlphanumeric = false; //optional
//    config.SignIn.RequireConfirmedEmail = true; //optional
//})
//.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



builder.Services.AddScoped<IPersonRepository<Person>, EFPersonRepository>();
builder.Services.AddScoped<IVacancyRepository<Vacancy>, EFVacancyRepository>();
builder.Services.AddScoped<IEmployeeRepository<Employee>, EFEmployeeRepository>();
builder.Services.AddScoped<IFeedbackRepository<Feedback>, EFFeedbackRepository>();
builder.Services.AddScoped<IInviteRepository<Invitation>, EFInviteRepository>();
builder.Services.AddScoped<IEducationRepository<Education>, EFEducationRepository>();
builder.Services.AddScoped<ISkillRepository<Skill>, EFSkillRepository>();
builder.Services.AddScoped<IAreaActivityRepository<AreaActivity>, EFAreaActivityRepository>();

// устраняем ошибку Cannot write DateTime with Kind=Local to PostgreSQL type 'timestamp with time zone', only UTC is supported. 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

var RoleManager = builder.Services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
//var UserManager = builder.Services.BuildServiceProvider().GetRequiredService<UserManager<IdentityUser>>();
var EmployeeManager = builder.Services.BuildServiceProvider().GetRequiredService<EmployeeManager>();
var VacancyManager = builder.Services.BuildServiceProvider().GetRequiredService<IVacancyRepository<Vacancy>>();
var EducationManager = builder.Services.BuildServiceProvider().GetRequiredService<IEducationRepository<Education>>();
var SkillManager = builder.Services.BuildServiceProvider().GetRequiredService<ISkillRepository<Skill>>();
var AreaActivityManager = builder.Services.BuildServiceProvider().GetRequiredService<IAreaActivityRepository<AreaActivity>>();
var PersonManager = builder.Services.BuildServiceProvider().GetRequiredService<PersonManager>();

await RoleConfig.CreateRoles(RoleManager);
await RoleConfig.CreateUsers(RoleManager, EmployeeManager, VacancyManager);
await RoleConfig.CreateApplicants(RoleManager, PersonManager);
await RoleConfig.CreateEducations(EducationManager);
await RoleConfig.CreateSkills(SkillManager);
await RoleConfig.CreateActivites(AreaActivityManager);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();



app.Run();


