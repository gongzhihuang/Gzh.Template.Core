using System;
using Gzh.Template.Core.Application.Jobs;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Gzh.Template.Core.Application
{
    using IOCContainer = IServiceProvider;
    /// <summary>
    /// Quartz startup.  asp.net core中整合quartz的启动类，在Startup中启动
    /// </summary>
    public class QuartzStartup
    {
        public IScheduler _scheduler { get; set; }

        private readonly ILogger _logger;
        private readonly IJobFactory iocJobfactory;
        public QuartzStartup(IOCContainer IocContainer, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<QuartzStartup>();
            iocJobfactory = new IOCJobFactory(IocContainer);
            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = iocJobfactory;
        }

        /// <summary>
        /// Start this instance.  作业调度
        /// </summary>
        public void Start()
        {
            _logger.LogInformation("Schedule job load as application start.");
            _scheduler.Start().Wait();

            //var HelloJob = JobBuilder.Create<HelloJob>()
            //   .WithIdentity("helloJob")
            //   .Build();

            //var HelloJobTrigger = TriggerBuilder.Create()
            //    .WithIdentity("helloJobTrigger")
            //    .StartNow()
            //    // 每分钟执行一次
            //    .WithCronSchedule("0 0/1 * * * ?")
            //    .Build();
            //_scheduler.ScheduleJob(HelloJob, HelloJobTrigger).Wait();

            //_scheduler.TriggerJob(new JobKey("UsageCounterSyncJob"));
        }

        public void Stop()
        {
            if (_scheduler == null)
            {
                return;
            }

            //if (_scheduler.Shutdown(waitForJobsToComplete: true).Wait(30000))
            //    _scheduler = null;
            //else
            //{
            //}
            _logger.LogCritical("Schedule job upload as application stopped");
        }
    }

    /// <summary>
    /// IOCJobFactory ：实现在Timer触发的时候注入生成对应的Job组件
    /// </summary>
    public class IOCJobFactory : IJobFactory
    {
        protected readonly IOCContainer Container;

        public IOCJobFactory(IOCContainer container)
        {
            Container = container;
        }

        //Called by the scheduler at the time of the trigger firing, in order to produce
        //     a Quartz.IJob instance on which to call Execute.
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return Container.GetService(bundle.JobDetail.JobType) as IJob;
        }

        // Allows the job factory to destroy/cleanup the job if needed.
        public void ReturnJob(IJob job)
        {
        }
    }
}

