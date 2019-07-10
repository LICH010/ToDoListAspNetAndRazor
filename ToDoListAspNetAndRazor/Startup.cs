using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ToDoListAspNetAndRazor.Config;
using ToDoListAspNetAndRazor.Contexts;
using ToDoListAspNetAndRazor.Entities;
using ToDoListAspNetAndRazor.Services;

namespace ToDoListAspNetAndRazor
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			var test = Configuration.GetSection(
				"IdentityServer").Get<IdentityServerSettings>();
			services.AddSingleton(Configuration.GetSection(
				"IdentityServer").Get<IdentityServerSettings>());

			services.AddDbContext<DataContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<UserEnt>()
				.AddEntityFrameworkStores<DataContext>()
				.AddDefaultTokenProviders();

			services.AddIdentityServer(opt =>
			{
				opt.PublicOrigin = IdentityServerSettings.ServerUrl;
				opt.Authentication.CookieAuthenticationScheme = "dummy";
			})
				.AddAspNetIdentity<UserEnt>()
				.AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
				.AddInMemoryClients(IdentityServerConfig.GetClients())
				.AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
				.AddDeveloperSigningCredential();

			services.AddAuthentication(opt =>
				{
					opt.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
					opt.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
				})
				.AddCookie("dummy")
				.AddIdentityServerAuthentication(opt =>
				{
					opt.Authority = IdentityServerSettings.ServerUrl;
					opt.ApiName = IdentityServerSettings.ApiName;
					opt.ApiSecret = IdentityServerSettings.ClientSecret;
					opt.RequireHttpsMetadata = false;
				});

			services.ConfigureApplicationCookie(opt =>
			{
				opt.Events.OnRedirectToLogin = ctx =>
				{
					ctx.Response.StatusCode = 401;
					return Task.CompletedTask;
				};
			});

			services.AddScoped<IAccountsService, AccountsService>();

			services.AddMvc()
				.AddJsonOptions(opts =>
				{
					opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				})
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseCors(builder =>
			{
				builder.AllowAnyHeader();
				builder.AllowAnyMethod();
				builder.AllowAnyOrigin();
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}
			
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseIdentityServer();

			app.UseMvc();
		}
	}
}
