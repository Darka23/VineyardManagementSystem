using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using VineyardManagementSystem.Data;
using VineyardManagementSystem.Repositories;
using VineyardManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false; 
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequiredLength = 6;
    options.SignIn.RequireConfirmedAccount = false;

}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IVineyardRepository, VineyardRepository>();
builder.Services.AddScoped<IGrapeVarietyRepository, GrapeVarietyRepository>();

builder.Services.AddScoped<IVineyardService, VineyardService>();
builder.Services.AddScoped<IGrapeVarietyService, GrapeVarietyService>();

builder.Services.AddScoped<IPlotRepository, PlotRepository>();
builder.Services.AddScoped<IPlotService, PlotService>();

builder.Services.AddScoped<IClimateRepository, ClimateRepository>();
builder.Services.AddScoped<IClimateService, ClimateService>();

builder.Services.AddScoped<IFieldActivityRepository, FieldActivityRepository>();
builder.Services.AddScoped<IFieldActivityService, FieldActivityService>();

builder.Services.AddScoped<IHarvestRepository, HarvestRepository>();
builder.Services.AddScoped<IHarvestService, HarvestService>();

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

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Автоматично прилагане на миграции (по желание)
    context.Database.Migrate(); 

    // Извикване на сийдъра
    DbSeeder.Seed(context);
}

app.Run();
