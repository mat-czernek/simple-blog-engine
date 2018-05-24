﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Infrastructure;
using MyBlog.Models;
using MyBlog.Services;

namespace MyBlog
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"customSettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add configuration service
            services.AddApplicationInsightsTelemetry(Configuration);

            services.Configure<DefaultUserData>(options => Configuration.GetSection("DefaultUserData").Bind(options));

   
            // Adds MVC service into the service collection (dependency injection)
            services.AddMvc();

            string connectionString = Configuration.GetSection("DatabaseOptions").GetValue<string>("ConnectionString");

            if(string.IsNullOrEmpty(connectionString))
                connectionString = "BlogDB.sdb";

            services.AddScoped<IUserManagement, UserManagement>();

            services.AddScoped<ISignInManagement, SignInManagement>();

            services.AddScoped<IDatabaseProvider, DatabaseProvider>();

            services.AddScoped<IResourceManagement, ResourceManagement>();

            services.AddScoped<IRoleManagement, RoleManagement>();

            services.AddScoped<IFormatContent, FormatContent>();

  
            // Registers database context as a service in service collection (dependency injections)
            services.AddDbContext<BlogDatabaseContext>(options => options.UseSqlite("Data source =" + connectionString));

       
            // Adds the default identity system
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BlogDatabaseContext>().AddDefaultTokenProviders();

        
            // Registers message sending service
            services.AddTransient<IMessageService, FileMessageService>();

            // Registers service for file operations (file upload)
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resources")
                )
            );

            services.AddScoped<ISeedDatabase, SeedDatabase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ISeedDatabase seedDatabase)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Enables autentication capabilities
            app.UseAuthentication();

            // initalize databse with default data
            seedDatabase.Initalize();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
