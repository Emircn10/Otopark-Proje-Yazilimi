using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OtoparkSistemi.Data;
using OtoparkSistemi.Models;
using OtoparkSistemi.Services.Interfaces;
using OtoparkSistemi.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});

// App Services
builder.Services.AddScoped<IAracService, AracService>();
builder.Services.AddScoped<IAbonelikService, AbonelikService>();
builder.Services.AddScoped<IGirisKayitService, GirisKayitService>();
builder.Services.AddScoped<ITarifeService, TarifeService>();

var app = builder.Build();

// Seed Data + Users
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    SeedData.Initialize(db);

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    foreach (var role in new[] { "Admin", "Calisan" })
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    if (await userManager.FindByNameAsync("admin") == null)
    {
        var admin = new ApplicationUser { UserName = "admin", Email = "admin@otopark.com", AdSoyad = "Sistem Yöneticisi", EmailConfirmed = true };
        await userManager.CreateAsync(admin, "Admin123!");
        await userManager.AddToRoleAsync(admin, "Admin");
    }

    if (await userManager.FindByNameAsync("calisan") == null)
    {
        var calisan = new ApplicationUser { UserName = "calisan", Email = "calisan@otopark.com", AdSoyad = "Görevli Personel", EmailConfirmed = true };
        await userManager.CreateAsync(calisan, "Calisan123!");
        await userManager.AddToRoleAsync(calisan, "Calisan");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
