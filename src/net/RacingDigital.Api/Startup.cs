// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RacingDigital.DAL;
using System;
using System.Linq;

namespace WebApp_OpenIDConnect_DotNet
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
            // Add services to the container.
            var connectionString = Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            //    // Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1
            //    options.HandleSameSiteCookieCompatibility();
            //});

            //services.AddAuthentication(options =>
            //{
            //    // this “policy” will pick Bearer when there's a Bearer header,
            //    // otherwise fall back to the cookie/OIDC scheme
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = "B2cPolicy";
            //})
            //.AddPolicyScheme("B2cPolicy", "B2C Combined", options =>
            //{
            //    options.ForwardDefaultSelector = context =>
            //    {
            //        var auth = context.Request.Headers["Authorization"].FirstOrDefault();
            //        return auth?.StartsWith("Bearer ") == true
            //            ? JwtBearerDefaults.AuthenticationScheme
            //            : OpenIdConnectDefaults.AuthenticationScheme;
            //    };
            //});
            //services.AddMicrosoftIdentityWebAppAuthentication(Configuration, Constants.AzureAdB2C); // interactive login
            //services.AddMicrosoftIdentityWebApiAuthentication(Configuration, Constants.AzureAdB2C); // API JWT validation
            //services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
            //{
            //    options.ResponseType = OpenIdConnectResponseType.Code;
            //    options.Scope.Add(options.ClientId);
            //});

            // 1) One AddAuthentication, defaulting to our “B2cPolicy” policy scheme
            var authBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = "B2cPolicy";
                options.DefaultChallengeScheme = "B2cPolicy";
            })
            // 2) “Policy” that picks Bearer if there’s an Authorization header,
            //    otherwise falls back to OIDC + Cookie for interactive login
            .AddPolicyScheme("B2cPolicy", "B2C Combined", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    var auth = context.Request.Headers["Authorization"].FirstOrDefault();
                    return auth?.StartsWith("Bearer ") == true
                         ? JwtBearerDefaults.AuthenticationScheme
                         : OpenIdConnectDefaults.AuthenticationScheme;
                };
            });
            authBuilder.AddMicrosoftIdentityWebApp(Configuration, Constants.AzureAdB2C);
            // 4) Non-interactive Web API (reads the same AzureAdB2C settings, sets up Bearer)
            authBuilder.AddMicrosoftIdentityWebApi(Configuration, "AzureAdB2C_Api");
            services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.Scope.Add(options.ClientId);
            });
            services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            services.AddRazorPages();

            services.AddOptions();
            services.Configure<OpenIdConnectOptions>(Configuration.GetSection("AzureAdB2C"));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod());
            });
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
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}