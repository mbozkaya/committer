using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;

namespace CommitterService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public IServiceScopeFactory _serviceScopeFactory { get; private set; }
        private string Schedule => "*/60 * * * * *"; //Runs every 60 seconds
        private CrontabSchedule _schedule;
        private DateTime _nextRun;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);

                if (now > _nextRun)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    new Commit();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(10000, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
