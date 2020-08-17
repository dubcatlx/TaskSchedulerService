using Hangfire;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSchedulerService
{
    public class JobService : IJobService
    {
        private readonly Configuration _configuration;
        private readonly IHttpService _httpService;

        public JobService
        (
            Configuration configuration,
            IHttpService httpService
        )
        {
            _configuration = configuration;
            _httpService = httpService;
        }

        public void Configure()
        {
            _configuration.Jobs
                .Where(job => job.Inactive)
                .ToList()
                .ForEach(job => RecurringJob.RemoveIfExists(job.Id));

            _configuration.Jobs
                .Where(job => job.Active)
                .ToList()
                .ForEach(job => RecurringJob.AddOrUpdate(job.Id, () => ExecuteAsync(job), job.Cron));
        }

        [AutomaticRetry(Attempts = 0)]
        public Task ExecuteAsync(Job job)
        {
            return _httpService.SendAsync(job.Method, job.Url);
        }
    }
}
