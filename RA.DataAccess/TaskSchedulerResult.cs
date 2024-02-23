namespace RA.DataAccess
{
    public class TaskSchedulerResult
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public required string CronExpression { get; set; }
        public required string Timestamp { get; set; }
        public required string Result { get; set; }
    }
}