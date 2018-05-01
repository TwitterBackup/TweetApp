using AutoMapper;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using TwitterBackup.Data;
using TwitterBackup.Data.Repository;
using TwitterBackup.Infrastructure.Providers;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.ApiClient;
using TwitterBackup.Services.ApiClient.Contracts;
using TwitterBackup.Services.Data;
using TwitterBackup.Services.Data.Contracts;
using TwitterBackup.Services.Email;
using TwitterBackup.Services.TwitterAPI;
using TwitterBackup.Services.TwitterAPI.Contracts;

namespace TwitterBackup.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TwitterDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TwitterDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = this.Configuration["Authentication:Twitter:ConsumerKey"];
                twitterOptions.ConsumerSecret = this.Configuration["Authentication:Twitter:ConsumerSecret"];
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                //options.Filters.Add(new RequireHttpsAttribute());
            });
            services.AddAutoMapper();

            services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
            services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IMappingProvider, MappingProvider>();
            services.AddTransient<ITweeterService, TweeterService>();
            services.AddTransient<ITweetService, TweetService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITweetApiService, TweetApiService>();
            services.AddTransient<ITweeterApiService, TweeterApiService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IApiClient, ApiClient>();

            var tokens = new RequestToken();
            services.AddScoped<ITwitterAuthenticator>(provider =>
                new TwitterAuthenticator(
                    this.Configuration["Authentication:Twitter:ConsumerKey"],
                    this.Configuration["Authentication:Twitter:ConsumerSecret"],
                    this.Configuration["Authentication:Twitter:AccessToken"],
                    this.Configuration["Authentication:Twitter:AccessTokenSecret"]
                ));
            services.AddScoped<IJsonProvider, JsonProvider>();
            services.AddScoped<IRestClient, RestClient>();
            services.AddScoped<IRestRequest, RestRequest>();
            services.AddScoped<IUriFactory, UriFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //var options = new RewriteOptions()
            //    //.AddRedirectToHttpsPermanent()
            //    .AddRedirectToHttps(301, 44342);
            //app.UseRewriter(options);

            app.UseStaticFiles();


            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

        }
    }
}
