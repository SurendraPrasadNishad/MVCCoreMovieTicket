using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineMovieTicket.Data;
using OnlineMovieTicket.Data.Services.actor;
using OnlineMovieTicket.Data.Services.producer;
using OnlineMovieTicket.Data.Services.cinema;
using OnlineMovieTicket.Data.Services.movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineMovieTicket.Data.Cart;
using Microsoft.AspNetCore.Http;
using OnlineMovieTicket.Data.Services.orders;
using OnlineMovieTicket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace OnlineMovieTicket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //add dbcontext parameterhereyou should pass is the data storage
            //   services.AddDbContext<ApplicationDBContext>(Options=>Options.GetServices<>
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            //Adding Actor Service
            services.AddScoped<IActorsService, ActorsService>();
            //Adding Producer Service
            services.AddScoped<IProducersService, ProducersService>();
            //Adding Cinema Service
            services.AddScoped<ICinemasService, CinemasService>();
            //Adding Movie Service
            services.AddScoped<IMoviesService, MoviesService>();
            //Adding Orders Service
            services.AddScoped<IOrdersService, OrdersService>();
            //Adding Identity Services for Auth authen
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>();
            //Adding Memory Cache
            services.AddMemoryCache();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
            //Adding ShoppingCart and ShoppingCart.GetShoppingCart(service) method and IHttpContextAccessor As service
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
            //Adding session to use in  ShoppingCart and ShoppingCart.GetShoppingCart(service) method  As service
            services.AddSession();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            //using Session for this app
            app.UseSession();
            //use authencation and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Movies}/{action=Index}/{id?}");
            });
          //AppInitializer.seed(app);
          AppInitializer.SeedUsersAndRolesAsync(app).Wait();

        }
    }
}
