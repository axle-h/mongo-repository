using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Breakfast.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
                   .Build()
                   .Run();
        }
    }
}
