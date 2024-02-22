using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA.DataAccess
{
    public interface ITaskSchedulerRepository
    {
        public Task<List<TaskSchedulerResult>> GetAll();
        public Task SaveTaskScheduler(TaskSchedulerResult taskSchedulerResult);
    }
}
