using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gzh.Template.Core.Repository.Core;

namespace Gzh.Template.Core.Repository.Domain.MysqlEntity
{
    public class ScheduleEntity : EntityMysql
    {
        public ScheduleEntity()
        {
        }

        /// <summary>
        /// Job的Key，在组内唯一
        /// </summary>
        /// <value>The job key.</value>
        [Column("job_key")]
        public string JobKey { get; set; }

        /// <summary>
        /// Job的组名
        /// </summary>
        /// <value>The job group.</value>
        [Column("job_group")]
        public string JobGroup { get; set; }

        /// <summary>
        /// trigger的Key,在组内唯一
        /// </summary>
        /// <value>The trigger key.</value>
        [Column("trigger_key")]
        public string TriggerKey { get; set; }

        /// <summary>
        /// trigger的组名
        /// </summary>
        /// <value>The trigger group.</value>
        [Column("trigger_group")]
        public string TriggerGroup { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value>The time create.</value>
        [Column("time_create")]
        public DateTime TimeCreate { get; set; }

        /// <summary>
        /// 开始执行时间
        /// </summary>
        /// <value>The time start.</value>
        [Column("time_start")]
        public DateTime TimeStart { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <value>The time end.</value>
        [Column("time_end")]
        public DateTime TimeEnd { get; set; }

        /// <summary>
        /// 重复执行次数
        /// </summary>
        /// <value>The repet count.</value>
        [Column("repeat_count")]
        public int RepeatCount { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        /// <value>The state of the run.</value>
        [Column("run_state")]
        public string RunState { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        /// <value>The job description.</value>
        [Column("job_description")]
        public string JobDescription { get; set; }
    }
}
