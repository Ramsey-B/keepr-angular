using System.Data;
using System.Threading.Tasks;
using keepr_angular.Repositories;
using keepr_angular.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace keepr_angular
{
  public class Startup
  {
    private readonly string _connectionString;
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      _connectionString = configuration.GetSection("DB").GetValue<string>("MySQLConnectionString");
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/dist";
      });

      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
      {
        options.LoginPath = "/Account/Login/";
        options.Events.OnRedirectToLogin = (context) =>
                  {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                  };
      });

      services.AddCors(options =>
      {
        options.AddPolicy("CorsDevPolicy", builder =>
              {
                builder
                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
              });
      });


      services.AddMvc();
      services.AddTransient<IDbConnection>(x => CreateDbContext());
      services.AddTransient<UserRepository>();
      services.AddTransient<VaultRepository>();
      services.AddTransient<KeepRepository>();
    }

    private IDbConnection CreateDbContext()
    {
      var connection = new MySqlConnection(_connectionString);
      connection.Open();
      return connection;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseCors("CorsDevPolicy");
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseSpaStaticFiles();
      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseAuthentication();
      app.UseMvc();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa =>
      {
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501

        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseAngularCliServer(npmScript: "start");
        }
      });
    }
  }
}
