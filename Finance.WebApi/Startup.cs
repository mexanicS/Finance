using System.Reflection;
using Finance.Application;
using Finance.Application.Clinents.Commands.CreateClient;
using Finance.Application.Clinents.Queries.GetClientList;
using Finance.Application.Common.Mappings;
using Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using Finance.Application.Interfaces;
using Finance.Domain;
using Finance.Persistence;
using Finance.WebApi.MiddleWare;
using Finance.WebApi.Models;
using MediatR;

namespace Finance.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddAutoMapper(config => {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(FinanceDbContext).Assembly));
                config.CreateMap<Client, ClientLookupDto>();
                config.CreateMap<CreateClientDto, CreateClientCommand>();
            });*/

            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(config => {
                config.RoutePrefix = string.Empty;
                config.SwaggerEndpoint("swagger/v1/swagger.json", "Finance API");
            });



            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
