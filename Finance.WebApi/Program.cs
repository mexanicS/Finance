using Finance.Persistence;

namespace Finance.WebApi
{
    public class Program
    {
        public static void Main(string[] args) 
        { 
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<FinanceDbContext>();
                    DbInitiliaziation.Initilize(context);
                }
                catch (Exception exception){ }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}

