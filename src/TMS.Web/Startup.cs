using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TMS.Shared;
using TMS.Tickets.Application.Handlers;
using TMS.Tickets.Config;
using TMS.Tickets.Integration.Queries;
using TMS.Tickets.Persistence;

namespace TMS.Web
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
            services.AddDbContextPool<TicketsDbContext>(options =>
                options.UseInMemoryDatabase("in-memory"));

            services.AddMediatR(
                typeof(GetTicketQuery).Assembly,
                typeof(GetTicketQueryHandler).Assembly);

            services.AddAutoMapper(
                typeof(AutomapperTicketsProfile).Assembly);

            RegisterServices(services);

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<MachineDateTime>();
        }
    }
}