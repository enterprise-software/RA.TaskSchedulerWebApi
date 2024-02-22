using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA.DataAccess
{
    public class TaskSchedulerRepository : ITaskSchedulerRepository
    {
        public async Task<List<TaskSchedulerResult>> GetAll()
        {
            using var context = new TaskSchedulerDbContext();
            var taskSchedulerList = await context.TaskSchedulerResults.ToListAsync();
            return taskSchedulerList;
        }

        public async Task SaveTaskScheduler(TaskSchedulerResult taskSchedulerResult)
        {
            using var context = new TaskSchedulerDbContext();
            context.TaskSchedulerResults.Add(taskSchedulerResult);
            await context.SaveChangesAsync();
        }

    }
}
