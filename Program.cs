using E_Commerce2.Data;
using E_Commerce2.Models;
using E_Commerce2.Services.IServices;
using E_Commerce2.Services.MServices;
using E_Commerce2.UnitOfWorkk;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>
                (options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("cs")));


            builder.Services.AddIdentity<AppUser, IdentityRole>( options =>

            {
                      options.Password.RequiredLength = 6;
                      options.Password.RequireNonAlphanumeric = false;
                      options.Password.RequireUppercase = false;
                      options.Password.RequireLowercase = false;
            })
    .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect here when unauthorized
    });


            var app = builder.Build();





            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
