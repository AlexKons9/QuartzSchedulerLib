
using System.Timers;
using Timer = System.Timers.Timer;

namespace QuartzConApp
{
    public class TimeSchedulerService
    {
        private Timer _timer;

        public int IntervalInMilliseconds { get; set; }
        public Action ActionToBeExecuted { get; set; }

        public TimeSchedulerService(int intervalInMilliseconds, Action actionToBeExecuted)
        {
            IntervalInMilliseconds = intervalInMilliseconds;
            ActionToBeExecuted = actionToBeExecuted;

            // Initialize the timer
            _timer = new Timer(intervalInMilliseconds);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true; // Set to false if you want a one-time execution
        }

        public async Task StartTimer()
        {
            if (_timer != null)
            {
                _timer.Start();
            }
            else
            {
                throw new InvalidOperationException("Timer has not been initialized. Call SetTimer before starting.");
            }
        }

        public async Task StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            ActionToBeExecuted.Invoke();
        }
    }
}
