using System.Collections.Generic;

namespace TaskSchedulerService
{
    public sealed class Configuration
    {
        public IReadOnlyList<Job> Jobs { get; set; }
    }
}
