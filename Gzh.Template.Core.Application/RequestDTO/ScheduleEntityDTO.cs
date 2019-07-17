using System;
namespace Gzh.Template.Core.Application.RequestDTO
{
    public class ScheduleEntityDTO
    {
        /// <summary>
        /// 开始执行时间
        /// </summary>
        /// <value>The time start.</value>
        public DateTime TimeStart { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <value>The time end.</value>
        public DateTime TimeEnd { get; set; }

        /// <summary>
        /// 重复执行次数
        /// </summary>
        /// <value>The repet count.</value>
        public int RepeatCount { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        /// <value>The job description.</value>
        public string JobDescription { get; set; }
    }
}
