using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskSchedulerService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(IApplicationBuilder application, IJobService jobService)
        {
            application.UseHsts();
            application.UseHttpsRedirection();
            application.UseRouting();
            application.UseEndpoints(builder => builder.MapControllers());
            application.UseHangfireDashboard();
            jobService.Configure();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<IJobService, JobService>();
            services.AddControllers();

            var configuration = new Configuration();
            _configuration.Bind(nameof(Configuration), configuration);
            services.AddSingleton(configuration);

            var connectionString = _configuration.GetConnectionString("Database");
            services.AddHangfire(config => config.UseSqlServerStorage(connectionString));
            services.AddHangfireServer();
        }
    }
}
