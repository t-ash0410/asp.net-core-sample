using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hello
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
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            var searchUrl = Environment.GetEnvironmentVariable("ELASTIC_SEARCH_SERVER");
            services.AddScoped<Lib.Infrastructure.DataAccessContext>((s) => new Lib.Infrastructure.DataAccessContext(connectionString));
            services.AddScoped<Lib.Infrastructure.BigDataAccessContext>((s) => new Lib.Infrastructure.BigDataAccessContext(searchUrl));
            services.AddScoped<Lib.Books.Repository.IBookRepository, Lib.Books.Repository.BookRepository>();
            services.AddScoped<Lib.Books.Repository.IBookSearchRepository, Lib.Books.Repository.BookSearchRepository>();
            services.AddScoped<Lib.Books.UseCase.IBookInteractor, Lib.Books.UseCase.BookInteractor>();
            services.AddScoped<Filters.DataAccessFilterBase, Filters.DataAccessFilter>();
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
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
