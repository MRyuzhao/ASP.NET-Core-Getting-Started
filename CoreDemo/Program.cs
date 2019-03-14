using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CoreDemo
{
    public class Program
    {
        public static void Main(string[] args)//程序运行入口
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration((context, builder) =>
                //{
                //})
                .UseStartup<Startup>();
    }
}
