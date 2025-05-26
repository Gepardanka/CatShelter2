global using IdType = int;
using CatShelter.Data;
using CatShelter.Models;
using CatShelter.Repository;
using CatShelter.Services;
using CatShelter.ViewModels.UserViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole<IdType>>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IRepository<Adoption>, AdoptionRepository>();
builder.Services.AddScoped<IAdoptionService, AdoptionService>();
builder.Services.AddScoped<IRepository<Cat>, CatRepository>();
builder.Services.AddScoped<ICatService, CatService>();

builder.Services.AddScoped<IValidator<CreateViewModel>, CreateViewModelValidator>();
builder.Services.AddScoped<IValidator<EditViewModel>, EditViewModelValidator>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ElevatedPrivileges",
         policy => policy.RequireRole(["Admin", "Employee"]));
    options.AddPolicy("Basic",
        policy => policy.RequireRole(["Admin", "Employee", "User"]));
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();


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
app.UseRouting();

app.UseAuthorization();

var supportedCultures = new[] { 
    new CultureInfo("en-GB"), 
    new CultureInfo("pl-PL"), 
    new CultureInfo("pt-BR")
};
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
localizationOptions.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());

app.UseRequestLocalization(localizationOptions);

// using (var scope = app.Services.CreateScope())
// {
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<IdType>>>();
//     string[] roleNames = { "Admin", "User", "Employee" };

//     foreach (var roleName in roleNames)
//     {
//         if (!await roleManager.RoleExistsAsync(roleName))
//         {
//             await roleManager.CreateAsync(new IdentityRole<IdType>(roleName));
//         }
//     }
// }

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
