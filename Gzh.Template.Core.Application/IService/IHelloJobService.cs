using System;
using System.Threading.Tasks;
using Gzh.Template.Core.Repository.Domain.MysqlEntity;

namespace Gzh.Template.Core.Application.IService
{
    public interface IHelloJobService
    {
        /// <summary>
        /// 添加一个job
        /// </summary>
        /// <returns>The schedule job.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        Task<ScheduleEntity> AddScheduleJob(ScheduleEntity scheduleEntity);

        /// <summary>
        /// 运行一个job
        /// </summary>
        /// <returns><c>true</c>, if schedule job was run, <c>false</c> otherwise.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        Task<bool> RunScheduleJobAsync(ScheduleEntity scheduleEntity);

        /// <summary>
        /// 停止并删除一个job
        /// </summary>
        /// <returns><c>true</c>, if schedule job was stoped, <c>false</c> otherwise.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        Task<bool> RemoveScheduleJobAsync(ScheduleEntity scheduleEntity);

        /// <summary>
        /// 暂停一个job
        /// </summary>
        /// <returns><c>true</c>, if schedule was paused, <c>false</c> otherwise.</returns>
        /// <param name="scheduleEntity">Schedule entity.</param>
        Task<bool> PauseScheduleJobAsync(ScheduleEntity scheduleEntity);
    }
}
