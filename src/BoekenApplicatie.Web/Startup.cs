using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BoekenApplicatie.Data.Context;
using BoekenApplicatie.Domain.Models;
using BoekenApplicatie.Web.Configuration;
using BoekenApplicatie.Web.Options;
using BoekenApplicatie.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BoekenApplicatie.Web
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
      services
        .Configure<CookiePolicyOptions>(ConfigureCookiePolicy)
        .AddDbContext<LibraryContext>(ConfigureDbContext)
        .ConfigureApplicationCookie(ConfigureApplicationCookie)
        .AddIdentity<ApplicationUser, IdentityRole<Guid>>(ConfigureIdentity)
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<LibraryContext>();

      services
        .Configure<SecurityStampValidatorOptions>(ConfigureSecurityStamp)
        .Configure<MailOptions>(ConfigureMail)
        .AddSingleton<IMailservice, SendGridMailService>()
        .AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services
        .AddAutoMapper(typeof(Startup));
    }

  private static void ConfigureCookiePolicy(CookiePolicyOptions options)
    {
      // This lambda determines whether user consent for non-essential cookies is needed for a given request.
      options.CheckConsentNeeded = context => true;
      options.MinimumSameSitePolicy = SameSiteMode.None;
    }
    private void ConfigureDbContext(DbContextOptionsBuilder options)
    {
      options.UseLazyLoadingProxies();
      options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    }
    private static void ConfigureIdentity(IdentityOptions options)
    {
      options.Password.RequireDigit = false;
      options.Password.RequiredLength = 12;
      options.Password.RequiredUniqueChars = 0;
      options.Password.RequireLowercase = false;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequireUppercase = false;
     }

    private static void ConfigureApplicationCookie(CookieAuthenticationOptions options)
    {
      options.AccessDeniedPath = "/Account/AccessDenied";
      options.Cookie.Name = "BoekenAppCookie";
      options.Cookie.HttpOnly = true;
      options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
      options.LoginPath = "/Account/Login";
      // ReturnUrlParameter requires `using Microsoft.AspNetCore.Authentication.Cookies;`
      options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
      options.SlidingExpiration = true;
    }

    private void ConfigureMail(MailOptions options)
    {
      options.ApiKey = Configuration.GetSection("SENDGRID_API_KEY").Value;
      options.FromAddress = Configuration.GetValue<string>("Mail:FromAddress");
      options.FromAddressName = Configuration.GetValue<string>("Mail:FromAddressName");
    }

    private static void ConfigureSecurityStamp(SecurityStampValidatorOptions options)
    {
      // enables logout, after updating the user's stat.
      options.ValidationInterval = TimeSpan.FromMinutes(5);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider service)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });

      RoleProfile.CreateUserRoles(service);
    }
  }
}