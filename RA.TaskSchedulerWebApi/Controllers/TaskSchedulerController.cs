using Microsoft.AspNetCore.Mvc;
using Quartz.Impl;
using Quartz;
using RA.DataAccess;

namespace RA.TaskSchedulerWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskSchedulerController(ILogger<TaskSchedulerController> logger, ISchedulerFactory schedulerFactory, ITaskSchedulerRepository taskSchedulerRepository) : ControllerBase
    {

        IScheduler? scheduler;
        private readonly ISchedulerFactory schedulerFactory = schedulerFactory;
        private readonly ILogger<TaskSchedulerController> _logger = logger;
        private readonly ITaskSchedulerRepository _taskSchedulerRepository = taskSchedulerRepository;

        [HttpPost(Name = "Run")]
        public async Task<bool> PostAsync(string cronExpression, string url)
        {
            try
            {
                _logger.LogInformation("Running the job");
                scheduler = await schedulerFactory.GetScheduler();
                // and start it off
                await scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<WebScrapingJob>()
                    .WithIdentity("jobRockwellAutomation", "groupRockwellAutomation")
                    .UsingJobData("url", url)
                    .UsingJobData("cronExpression", cronExpression)
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("triggerRockwellAutomation", "groupRockwellAutomation")
                    .StartNow()
                    .WithCronSchedule(cronExpression)
                    .Build();

                // Tell Quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);
                return true;
            }
            catch (Exception except)
            {
                _logger.LogError(except.Message);
                return false;
            }
 
        }

        [HttpGet(Name = "Shutdown")]
        public async Task<List<TaskSchedulerResult>> GetAsync()
        {
            try
            {
                // and last shut down the scheduler when you are ready to close your program
                scheduler = await schedulerFactory.GetScheduler();
                await scheduler!.Shutdown();

                //await Console.Out.WriteLineAsync("Greetings from WebScrapingJob was stopped!");
                _logger.LogWarning("The job was stopped!");
                //_logger.LogInformation("Greetings from WebScrapingJob was stopped!");

                var results = await _taskSchedulerRepository.GetAll();
                return results;
            }
            catch (Exception except)
            {
                _logger.LogError(except.Message);
                return [];
            }
       }
    }
}
