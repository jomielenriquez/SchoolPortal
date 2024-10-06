using Portal.Data.Data;
using Microsoft.EntityFrameworkCore;
using Portal.Data;
using Portal.Services;
using Portal.Data.Interface;
using Portal.Data.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add cookie-based authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login"; // Redirect to login page if not authenticated
    });

builder.Services.AddDbContext<PortalDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();


builder.Services.AddScoped<ISystemParameterRepository, SystemParameterRepository>();
builder.Services.AddScoped<ISystemParameterService, SystemParameterService>();

builder.Services.AddScoped<INewsAndAnnouncementsService, NewsAndAnnouncementsService>();
builder.Services.AddScoped<INewsAndAnnouncementsRepository, NewsAndAnnouncementsRepository>();

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
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
