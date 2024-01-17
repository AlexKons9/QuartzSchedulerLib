using NCrontab;
using Quartz;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzConApp
{
    public class CronExpressionService
    {

        public static List<string> SortCronExpressions(List<string> cronExpressions, bool ascending = true)
        {
            // Parse cron expressions into a sortable format (here, using TimeSpan)
            var sortedExpressions = cronExpressions
            .Select(expression => new { Expression = expression, Time = CronToTimeSpan(expression) })
            .OrderBy(item => item.Time)
            .Select(item => item.Expression)
            .ToList();

            if (!ascending)
            {
                sortedExpressions.Reverse();
            }

            return sortedExpressions;
        }


        public static TimeSpan? CronToTimeSpan(string cronExpression)
        {
            var expression = new CronExpression(cronExpression);
            var timeNow = DateTime.Now;
            var cronTime = expression.GetTimeAfter(timeNow);
            TimeSpan? timeUntilNextFire = cronTime - timeNow;
            return timeUntilNextFire;
        }

        public static DateTimeOffset? GetStartTimeFromCronExpression(string cronExpression)
        {
            var exp = new CronExpression(cronExpression);
            var d = DateTime.Now;
            var obj = exp.GetTimeAfter(DateTime.Now);
            return obj;
        }
    }
}
