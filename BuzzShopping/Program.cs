using BuzzShopping.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver")));

builder.Services
    .AddAuthorization(o =>
    {
        o.AddPolicy("RequireAdminStaff", policy =>
        policy.RequireRole("Administrador", "Staff"));
    });

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
    {
        o.Cookie.HttpOnly = true;
        o.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        o.LoginPath = "/Account/Login";
        o.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
