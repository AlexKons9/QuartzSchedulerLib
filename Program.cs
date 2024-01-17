// Setup the scheduler
using Quartz.Impl;
using Quartz;
using QuartzConApp;

SchedulerService schedulerService = new SchedulerService();

await schedulerService.StartScheduler();
IDictionary<string, object> data = new Dictionary<string, object>
{
    {"name", new { FirstName = "Alex"} }
};
await schedulerService.ScheduleJob<MyJob>("myJob", "0/10 * * ? * * *", data);
var x = CronExpressionService.GetStartTimeFromCronExpression("0/10 * * ? * * *");

Console.WriteLine("Press any key to stop the scheduler...");
Console.ReadKey();
await schedulerService.StopScheduler();

