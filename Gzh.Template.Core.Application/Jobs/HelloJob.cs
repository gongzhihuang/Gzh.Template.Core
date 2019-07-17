using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Gzh.Template.Core.Application.Jobs {
    /// <summary>
    /// quartz的作业类,在QuartzStartup中添加到调度程序中
    /// </summary>
    public class HelloJob : IJob, IJobListener, ITriggerListener, ISchedulerListener {
        private readonly ILogger _logger;

        public string Name { get; }

        public HelloJob (ILoggerFactory loggerFactory) {
            Name = this.GetType ().ToString ();
            _logger = loggerFactory.CreateLogger<QuartzStartup> ();
        }

        //实现IJob接口的Excute方法
        public async Task Execute (IJobExecutionContext context) {
            //context.
            _logger.LogInformation ("job名称：" + context.JobDetail.Key +
                "\n trigger开始时间：" + context.Trigger.StartTimeUtc +
                "\n trigger结束时间：" + context.Trigger.EndTimeUtc +
                "\n 当前时间:" + DateTime.Now +
                "\n 当前触发时间：" + context.ScheduledFireTimeUtc +
                "\n 下次触发时间：" + context.NextFireTimeUtc +
                "\n 传递的数据：" + context.JobDetail.JobDataMap["name"]
            );
            //await Console.Out.WriteLineAsync("Hello Job" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        #region
        public async Task JobExecutionVetoed (IJobExecutionContext context, CancellationToken cancellationToken) {
            _logger.LogInformation ("任务执行失败，重新执行");
            //任务执行失败，再次执行任务
            await Execute (context);
        }

        public async Task JobToBeExecuted (IJobExecutionContext context, CancellationToken cancellationToken) {
            _logger.LogInformation ("准备执行任务");
            //return null;
        }

        public async Task JobWasExecuted (IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken) {
            _logger.LogInformation ("任务执行完成");
        }
        #endregion
        #region
        public async Task TriggerComplete (ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken) {
            _logger.LogInformation ("触发器触发成功 \n ------------------------------------------------------------------------------------------------------------------");
        }

        public async Task TriggerFired (ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken) {
            _logger.LogInformation ("触发器开始触发");
        }

        public async Task TriggerMisfired (ITrigger trigger, CancellationToken cancellationToken) {
            _logger.LogInformation ("触发器触发失败");
        }

        public async Task<bool> VetoJobExecution (ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken) {
            _logger.LogInformation ("可以阻止任务执行");
            //true 阻止，false 不阻止
            return false;
        }
        #endregion

        #region
        public async Task JobScheduled (ITrigger trigger, CancellationToken cancellationToken) {
            _logger.LogInformation ("JobScheduled");
        }

        public async Task JobUnscheduled (TriggerKey triggerKey, CancellationToken cancellationToken) {
            _logger.LogInformation ("JobUnscheduled");
        }

        public async Task TriggerFinalized (ITrigger trigger, CancellationToken cancellationToken) {
            _logger.LogInformation ("TriggerFinalized");
        }

        public async Task TriggerPaused (TriggerKey triggerKey, CancellationToken cancellationToken) {
            _logger.LogInformation ("TriggerPaused");
        }

        public async Task TriggersPaused (string triggerGroup, CancellationToken cancellationToken) {
            _logger.LogInformation ("TriggersPaused");
        }

        public async Task TriggerResumed (TriggerKey triggerKey, CancellationToken cancellationToken) {
            _logger.LogInformation ("TriggerResumed");
        }

        public async Task TriggersResumed (string triggerGroup, CancellationToken cancellationToken) {
            _logger.LogInformation ("TriggersResumed");
        }

        public async Task JobAdded (IJobDetail jobDetail, CancellationToken cancellationToken) {
            _logger.LogInformation ("JobAdded");
        }

        public async Task JobDeleted (JobKey jobKey, CancellationToken cancellationToken) {
            _logger.LogInformation ("JobDeleted");
        }

        public async Task JobPaused (JobKey jobKey, CancellationToken cancellationToken) {
            _logger.LogInformation ("JobPaused");
        }

        public async Task JobInterrupted (JobKey jobKey, CancellationToken cancellationToken) {
            _logger.LogInformation ("JobInterrupted");
        }

        public async Task JobsPaused (string jobGroup, CancellationToken cancellationToken) {
            _logger.LogInformation ("JobsPaused");
        }

        public async Task JobResumed (JobKey jobKey, CancellationToken cancellationToken) {
            _logger.LogInformation ("JobResumed");
        }

        public async Task JobsResumed (string jobGroup, CancellationToken cancellationToken) {
            _logger.LogInformation ("JobsResumed");
        }

        public async Task SchedulerError (string msg, SchedulerException cause, CancellationToken cancellationToken) {
            _logger.LogInformation ("SchedulerError");
        }

        public async Task SchedulerInStandbyMode (CancellationToken cancellationToken) {
            _logger.LogInformation ("SchedulerInStandbyMode");
        }

        public async Task SchedulerStarted (CancellationToken cancellationToken) {
            _logger.LogInformation ("SchedulerStarted");
        }

        public async Task SchedulerStarting (CancellationToken cancellationToken) {
            _logger.LogInformation ("SchedulerStarting");
        }

        public async Task SchedulerShutdown (CancellationToken cancellationToken) {
            _logger.LogInformation ("SchedulerShutdown");
        }

        public async Task SchedulerShuttingdown (CancellationToken cancellationToken) {
            _logger.LogInformation ("SchedulerShuttingdown");
        }

        public async Task SchedulingDataCleared (CancellationToken cancellationToken) {
            _logger.LogInformation ("SchedulingDataCleared");
        }
        #endregion
    }

    /// <summary>
    /// 要在startup中注入
    /// </summary>
    public class HelloJobTest : IJob {
        private readonly ILogger _logger;

        public HelloJobTest (ILoggerFactory loggerFactory) {
            _logger = loggerFactory.CreateLogger<QuartzStartup> ();
        }

        //实现IJob接口的Excute方法
        public async Task Execute (IJobExecutionContext context) {
            _logger.LogInformation ("干");
            await Console.Out.WriteLineAsync ("Hello" + DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class MyJob : IJob //创建IJob的实现类，并实现Excute方法。
    {
        public Task Execute (IJobExecutionContext context) {
            return Task.Run (() => {
                Console.Out.WriteLine (DateTime.Now);
            });
        }
    }
}