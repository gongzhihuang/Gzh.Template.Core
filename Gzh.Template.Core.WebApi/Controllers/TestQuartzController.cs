using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gzh.Template.Core.Application.IService;
using Gzh.Template.Core.Application.Jobs;
using Gzh.Template.Core.Application.RequestDTO;
using Gzh.Template.Core.Application.Service;
using Gzh.Template.Core.Infrastructure;
using Gzh.Template.Core.Repository.Domain.MysqlEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace Gzh.Template.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestQuartzController : Controller
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

        private readonly IHelloJobService _helloJobService;

        public TestQuartzController(ISchedulerFactory schedulerFactory, IHelloJobService helloJobService)
        {
            _schedulerFactory = schedulerFactory;
            _helloJobService = helloJobService;
        }

        /// <summary>
        /// Tests the quartz.
        /// </summary>
        /// <returns>The quartz.</returns>
        [HttpGet("testQuartz")]
        public async Task<ActionResult> TestQuartz()
        {

            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            //2、开启调度器
            await _scheduler.Start();
            //3、创建一个触发器
            var trigger = TriggerBuilder.Create()
              .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())//每两秒执行一次
              .Build();
            //4、创建任务
            var jobDetail = JobBuilder.Create<HelloJobTest>()
              .WithIdentity("job", "group")
              .Build();
            //5、将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(jobDetail, trigger);
            return Ok();
        }

        [HttpGet("testQuartz2")]
        public async Task<ActionResult> TestQuartz2()
        {

            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            //2、开启调度器
            await _scheduler.Start();
            //3、创建一个触发器
            var trigger = TriggerBuilder.Create()
              .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever())//每两秒执行一次
              .Build();
            //4、创建任务
            var jobDetail = JobBuilder.Create<HelloJob>()
              .WithIdentity("job2", "group2")
              .Build();
            //5、将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(jobDetail, trigger);
            return Ok();
        }

        /// <summary>
        /// 添加一个HelloJob的调度任务
        /// </summary>
        /// <returns>The hello job.</returns>
        /// <param name="scheduleEntityDTO">Schedule entity dto.</param>
        [HttpPost("AddHelloJob")]
        public async Task<ActionResult<ApiResponse<ScheduleEntity>>> AddHelloJob([FromBody]ScheduleEntityDTO scheduleEntityDTO)
        {
            var result = new ApiResponse<ScheduleEntity>();
            try
            {
                ScheduleEntity scheduleEntity = new ScheduleEntity
                {
                    JobKey = Guid.NewGuid().ToString(),
                    JobGroup = "Hello",
                    TriggerKey = Guid.NewGuid().ToString(),
                    TriggerGroup = "Hello",
                    TimeCreate = DateTime.Now,
                    TimeStart = scheduleEntityDTO.TimeStart,
                    TimeEnd = scheduleEntityDTO.TimeEnd,
                    RepeatCount = scheduleEntityDTO.RepeatCount,
                    RunState = "未启动",
                    JobDescription = scheduleEntityDTO.JobDescription
                };

                ScheduleEntity scheduleEntityData = await _helloJobService.AddScheduleJob(scheduleEntity);
                result.Message = "添加成功";
                result.Result = scheduleEntityData;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 运行一个任务
        /// </summary>
        /// <returns>The hello job.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        [HttpPost("RunHelloJob")]
        public async Task<ActionResult<ApiResponse>> RunHelloJob([FromBody]ScheduleEntity scheduleEntity)
        {
            var result = new ApiResponse();
            try
            {
                bool res = await _helloJobService.RunScheduleJobAsync(scheduleEntity);

                if (res)
                {
                    result.Message = "运行成功";
                }
                else
                {
                    result.Code = 500;
                    result.Message = "运行失败";
                }
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <returns>The hello job.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        [HttpPost("PauseHelloJob")]
        public async Task<ActionResult<ApiResponse>> PauseHelloJob([FromBody]ScheduleEntity scheduleEntity)
        {
            var result = new ApiResponse();
            try
            {
                bool res = await _helloJobService.PauseScheduleJobAsync(scheduleEntity);

                if (res)
                {
                    result.Message = "暂停成功";
                }
                else
                {
                    result.Code = 500;
                    result.Message = "暂停失败";
                }
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 删除一个任务
        /// </summary>
        /// <returns>The hello job.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        [HttpDelete("DeleteHelloJob")]
        public async Task<ActionResult<ApiResponse>> DeleteHelloJob([FromBody]ScheduleEntity scheduleEntity)
        {
            var result = new ApiResponse();
            try
            {
                var res = await _helloJobService.RemoveScheduleJobAsync(scheduleEntity);
                if (res)
                {
                    result.Message = "删除成功";
                }
                else { result.Message = "删除失败"; }
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
    }
}