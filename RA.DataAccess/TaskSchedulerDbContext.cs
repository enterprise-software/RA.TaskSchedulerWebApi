using Microsoft.EntityFrameworkCore;

namespace RA.DataAccess
{
    public class TaskSchedulerDbContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TaskSchedulerDb");
        }
        public DbSet<TaskSchedulerResult> TaskSchedulerResults { get; set; }

    }
}
