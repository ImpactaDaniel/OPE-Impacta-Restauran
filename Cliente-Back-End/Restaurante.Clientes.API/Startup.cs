using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Restaurante.Application;
using Restaurante.Clientes.API.HostedServices;
using Restaurante.Clientes.Application.Hubs;
using Restaurante.Domain;
using Restaurante.Infra;

namespace Restaurante.Clientes.API
{
    public class Startup
    {
        private readonly static string CORS_NAME = "Default";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });

            services.AddApplication(Configuration);
            services.AddInfra(Configuration);
            services.AddDomain();

            services
                .AddCors(cors => cors
                                        .AddPolicy(CORS_NAME, policy => policy
                                                                        .WithOrigins("http://localhost:4200")
                                                                        .AllowAnyMethod()
                                                                        .AllowAnyHeader()
                                                                        .AllowCredentials()
                                                                        .Build()));
            services.AddControllers()
                    .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurante.Clientes.API", Version = "v1" });
            });
            services.AddHostedService<EventBusHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurante.Clientes.API v1"));
            }

            app.UseCors(CORS_NAME);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<InvoiceHub>("/invoice-hub");
            });

            app.ConfigureEventBus();
        }
    }
}
