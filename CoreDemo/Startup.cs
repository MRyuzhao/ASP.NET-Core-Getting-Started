using CoreDemo.Models;
using CoreDemo.Services;
using CoreDemo.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddSingleton<IRepository<Student>, InMomoryRepository>();

            services.Configure<ConnectionOptions>(_configuration.GetSection("ConnectionStrings"));
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

            app.UseStatusCodePages();
            app.UseStaticFiles();

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
