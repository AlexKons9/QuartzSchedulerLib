using Quartz;
using Quartz.Impl;

namespace QuartzConApp
{
    public class SchedulerService
    {
        private readonly IScheduler scheduler;

        public SchedulerService()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
        }

        public async Task StartScheduler()
        {
            await scheduler.Start();
            Console.WriteLine("Scheduler started.");
        }

        public async Task ScheduleJob<T>(string jobName, string intervalCronExpression, IDictionary<string, object> jobDataMap) where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>()
                                      .WithIdentity(jobName)
                                      .SetJobData(new JobDataMap(jobDataMap))
                                      .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                            .WithIdentity(jobName + "Trigger")
                                            .WithCronSchedule(intervalCronExpression)
                                            .Build();

            await scheduler.ScheduleJob(job, trigger);
            Console.WriteLine($"Job '{jobName}' scheduled to run every {intervalCronExpression} cron exp sec.");
        }

        public async Task StopScheduler()
        {
            await scheduler.Shutdown();
            Console.WriteLine("Scheduler stopped.");
        }
    }

}
