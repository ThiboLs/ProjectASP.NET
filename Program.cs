using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using ProjectASP.Models; // Vervang 'ProjectASP.Models' door de namespace waar je ApplicationUser-klasse zich bevindt

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(11, 3, 2))));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    // Wachtwoord vereisten
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    // Vergrendelingsbeleid
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // Duur van vergrendeling
    options.Lockout.MaxFailedAccessAttempts = 5; // Maximum aantal mislukte aanmeldpogingen voordat vergrendeling wordt toegepast
    options.Lockout.AllowedForNewUsers = true; // Vergrendeling toepassen op nieuwe gebruikers

    // Bevestigde e-mail vereist voor aanmelding
    options.SignIn.RequireConfirmedEmail = true;

    // Anderen
    options.User.RequireUniqueEmail = true; // Vereis uniek e-mailadres voor elke gebruiker
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
