using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzConApp
{
    public class MyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var data = context.JobDetail.JobDataMap;
            object obj;
            var b = data.TryGetValue("name", out obj);
            var n = obj.GetType().GetProperty("FirstName").GetValue(obj);
            Console.WriteLine("Job executed at: " + DateTime.Now + $" Property Value: {n}");
            return Task.CompletedTask;
        }
    }
}
