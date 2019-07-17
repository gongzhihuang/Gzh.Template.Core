using System;
using System.Threading.Tasks;
using Gzh.Template.Core.Application.IService;
using Gzh.Template.Core.Application.Jobs;
using Gzh.Template.Core.Repository;
using Gzh.Template.Core.Repository.Domain.MysqlEntity;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl.Matchers;

namespace Gzh.Template.Core.Application.Service
{
    public class HelloJobService : IHelloJobService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

        public BaseRepositoryMysql<ScheduleEntity> _baseRepositoryMysql;

        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        public HelloJobService(ISchedulerFactory schedulerFactory, BaseRepositoryMysql<ScheduleEntity> baseRepositoryMysql, ILoggerFactory loggerFactory)
        {
            _schedulerFactory = schedulerFactory;
            _baseRepositoryMysql = baseRepositoryMysql;
            _logger = loggerFactory.CreateLogger<QuartzStartup>();
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 添加一个job
        /// </summary>
        /// <returns>The schedule job.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        public async Task<ScheduleEntity> AddScheduleJob(ScheduleEntity scheduleEntity)
        {
            try
            {
                if (scheduleEntity != null)
                {
                    if (scheduleEntity.TimeStart == null)
                    {
                        scheduleEntity.TimeStart = DateTime.Now;
                    }
                    //DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(scheduleEntity.TimeStart, 1);
                    DateTimeOffset starRunTime = new DateTimeOffset(scheduleEntity.TimeStart, TimeSpan.Zero);
                    if (scheduleEntity.TimeEnd == null)
                    {
                        scheduleEntity.TimeEnd = DateTime.MaxValue.AddDays(-1);
                    }
                    //DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(scheduleEntity.TimeEnd, 1);
                    DateTimeOffset endRunTime = new DateTimeOffset(scheduleEntity.TimeEnd, TimeSpan.Zero);
                    _scheduler = await _schedulerFactory.GetScheduler();
                    await _scheduler.Start();

                    JobDataMap jobDataMap = new JobDataMap();
                    jobDataMap.Add("name", "jack");


                    IJobDetail job = JobBuilder.Create<HelloJob>()
                      .WithIdentity(scheduleEntity.JobKey, scheduleEntity.JobGroup)
                      .UsingJobData(jobDataMap)
                      .Build();
                    ITrigger trigger = TriggerBuilder.Create()
                                                    .StartAt(starRunTime)
                                                    .EndAt(endRunTime)
                                                    .WithIdentity(scheduleEntity.TriggerKey, scheduleEntity.TriggerGroup)
                                                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(scheduleEntity.RepeatCount - 1))
                                                    .Build();
                    await _scheduler.ScheduleJob(job, trigger);
                    //await _scheduler.Start();
                    //await PauseScheduleJobAsync(scheduleEntity);
                    //return true;


                    //第六步：创建任务监听，用来解决任务执行失败的情况. HelloJob类实现IJobListener接口
                    IJobListener jobListener = new HelloJob(_loggerFactory);

                    // 注: 任务监听是通过 IJobListener.Name 来区分的.以下逻辑避免多个任务监听情况下造成的监听被覆盖.
                    // a) 获取当前任务监听实例的名称.
                    var listener = _scheduler.ListenerManager.GetJobListener(jobListener.Name);
                    // b) 通过job.Key 获取该任务在调度系统中的唯一实体
                    IMatcher<JobKey> matcher = KeyMatcher<JobKey>.KeyEquals(job.Key);
                    // c) 注意: 任务监听系统中已存在当前任务监听实例,与新添加任务监听的逻辑的区别.
                    if (listener != null)
                    {
                        // 如果已存在该任务监听实例,调用此方法,为该任务监听实例新增监听对象
                        _scheduler.ListenerManager.AddJobListenerMatcher(jobListener.Name, matcher);
                    }
                    else
                    {
                        // 任务监听系统中不存在该任务监听实例,则调用此方法新增监听对象
                        _scheduler.ListenerManager.AddJobListener(jobListener, matcher);
                    }

                    //创建触发器监听，触发器监听与任务监听同名也不影响
                    ITriggerListener triggerListener = new HelloJob(_loggerFactory);
                    var triListener = _scheduler.ListenerManager.GetTriggerListener(triggerListener.Name);
                    IMatcher<TriggerKey> triMatcher = KeyMatcher<TriggerKey>.KeyEquals(trigger.Key);
                    if (triListener != null)
                    {
                        _scheduler.ListenerManager.AddTriggerListenerMatcher(triggerListener.Name, triMatcher);
                    }
                    else
                    {
                        _scheduler.ListenerManager.AddTriggerListener(triggerListener, triMatcher);
                    }

                    ISchedulerListener mySchedListener = new HelloJob(_loggerFactory);

                    _scheduler.ListenerManager.AddSchedulerListener(mySchedListener);
                }
                bool res = _baseRepositoryMysql.Add(scheduleEntity);
                if (res)
                {
                    await PauseScheduleJobAsync(scheduleEntity);
                    return scheduleEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 暂停某个调度任务
        /// </summary>
        /// <returns>The schedule job async.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        public async Task<bool> PauseScheduleJobAsync(ScheduleEntity scheduleEntity)
        {
            try
            {
                _scheduler = await _schedulerFactory.GetScheduler();

                await _scheduler.PauseJob(new JobKey(scheduleEntity.JobKey, scheduleEntity.JobGroup));

                ScheduleEntity scheduleEntityData = _baseRepositoryMysql.FindSingle(x => x.Id == scheduleEntity.Id);
                scheduleEntityData.RunState = "暂停中";
                _baseRepositoryMysql.Update(scheduleEntityData);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 开启某个job
        /// </summary>
        /// <returns>The schedule job async.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        public async Task<bool> RunScheduleJobAsync(ScheduleEntity scheduleEntity)
        {
            try
            {
                _scheduler = await _schedulerFactory.GetScheduler();

                await _scheduler.ResumeJob(new JobKey(scheduleEntity.JobKey, scheduleEntity.JobGroup));

                ScheduleEntity scheduleEntityData = _baseRepositoryMysql.FindSingle(x => x.Id == scheduleEntity.Id);
                scheduleEntityData.RunState = "已启动";
                _baseRepositoryMysql.Update(scheduleEntityData);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 停止并删除某个job
        /// </summary>
        /// <returns><c>true</c>, if schedule job was stoped, <c>false</c> otherwise.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        public async Task<bool> RemoveScheduleJobAsync(ScheduleEntity scheduleEntity)
        {
            try
            {
                _scheduler = await _schedulerFactory.GetScheduler();

                await _scheduler.DeleteJob(new JobKey(scheduleEntity.JobKey, scheduleEntity.JobGroup));

                var scheduleEntityData = _baseRepositoryMysql.FindSingle(x => x.JobKey == scheduleEntity.JobKey && x.JobGroup == scheduleEntity.JobGroup);
                bool res = _baseRepositoryMysql.Delete(scheduleEntityData);
                if (res)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
