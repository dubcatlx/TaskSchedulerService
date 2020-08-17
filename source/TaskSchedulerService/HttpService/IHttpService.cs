using System.Threading.Tasks;

namespace TaskSchedulerService
{
    public interface IHttpService
    {
        Task SendAsync(string method, string url);
    }
}
