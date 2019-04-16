using System.IO;
using CoreDemo.Services;
using CoreDemo.Settings;
using EfCore.Data;
using EfCore.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace CoreDemo
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //注册服务
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<MyContext>(options =>
            {
                options.EnableSensitiveDataLogging(true);
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), opts =>
                    {
                        opts.MaxBatchSize(1000);
                    });
            }, ServiceLifetime.Transient);

            services.AddScoped<IRepository<Student>, EfCoreRepository>();

            services.Configure<ConnectionOptions>(_configuration.GetSection("ConnectionStrings"));

            services.AddDbContext<IdentityDbContext>(opts =>
            {
                opts.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("CoreDemo"));
            });

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<IdentityDbContext>();

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredLength = 1;
                opts.Password.RequiredUniqueChars = 1;

                opts.User.RequireUniqueEmail = false;
            });
        }

        //处理http请求
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            app.Use(async (context, next) =>
            {
                logger.LogInformation($"==>begin in {env.EnvironmentName}");
                await next();
            });

            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules"))
            });

            app.UseAuthentication();

            app.UseMvc(builder =>
                {
                    builder.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });

            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("M1 start");
            //    await context.Response.WriteAsync("Hello World!");
            //    await next();
            //    logger.LogInformation("M1 end");
            //});

            //app.Run(async (context) =>
            //{
            //    logger.LogInformation("M2 start");
            //    await context.Response.WriteAsync("Another Hello World!");
            //    logger.LogInformation("M2 end");
            //});
        }
    }
}
