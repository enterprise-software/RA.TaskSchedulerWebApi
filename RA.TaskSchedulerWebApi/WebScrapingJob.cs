using HtmlAgilityPack;
using Quartz;
using RA.DataAccess;
using RA.TaskSchedulerServices;
using RA.TaskSchedulerServicesImp;
using System.Globalization;

namespace RA.TaskSchedulerWebApi
{
    public class WebScrapingJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {

            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string? url = dataMap.GetString("url");

            string? cronExpression = dataMap.GetString("cronExpression");

            await Console.Out.WriteLineAsync($"Instance (key) => {key} URL to scraping {url}");

            ScrapingService scrapingService = new();

            var response = await  scrapingService.GetResponse(url);

            TaskSchedulerRepository taskSchedulerRepository = new TaskSchedulerRepository();
            _ = taskSchedulerRepository.SaveTaskScheduler(new TaskSchedulerResult
            {
                Url = url,
                CronExpression = cronExpression,
                Timestamp = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture),
                Result = response,
            });
        }
    }
}
