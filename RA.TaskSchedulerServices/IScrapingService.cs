namespace RA.TaskSchedulerServices
{
    public interface IScrapingService
    {
        Task<string> GetResponse(string url);
    }
}
