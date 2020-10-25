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

namespace Web
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
      services.AddScoped<Lib.Infrastructure.DBContext>((s) => new Lib.Infrastructure.DBContext(connectionString));
      services.AddScoped<Lib.Infrastructure.ESContext>((s) => new Lib.Infrastructure.ESContext(searchUrl));
      services.AddScoped<Lib.Books.Repository.IBookRepository, Lib.Books.Repository.BookRepository>();
      services.AddScoped<Lib.Books.Repository.IBookSearchRepository, Lib.Books.Repository.BookSearchRepository>();
      services.AddScoped<Lib.Books.UseCase.IBookInteractor, Lib.Books.UseCase.BookInteractor>();
      services.AddScoped<Filters.DataAccessFilterBase, Filters.DataAccessFilter>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseDeveloperExceptionPage();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
