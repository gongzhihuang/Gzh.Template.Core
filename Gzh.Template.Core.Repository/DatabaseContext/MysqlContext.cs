using Gzh.Template.Core.Repository.Domain.MysqlEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gzh.Template.Core.Repository.DatabaseContext
{
    public class MysqlContext : DbContext
    {
        public MysqlContext(DbContextOptions<MysqlContext> options) : base(options)
        {

        }

        //自定义DbContext实体属性名与数据库表对应名称（默认 表名与属性名对应是 User与Users）
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Blog>().ToTable("blog");
            modelBuilder.Entity<Post>().ToTable("post");

            modelBuilder.Entity<ScheduleEntity>().ToTable("schedule_entity");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// 任务调度记录表
        /// </summary>
        /// <value>The schedule entitys.</value>
        public DbSet<ScheduleEntity> ScheduleEntitys { get; set; }
    }
}
