# Gzh.Template.Core 

asp.net core 2.2 

整合mongodb文档数据库,mysql数据库,Repository模式,Autofac依赖注入,swagger的api可视化,AutoMapper,Quartz.Net作业调度框架

分层：
* Gzh.Template.Core.Application （应用层）
应用程序的服务(业务)，服务的注入，DTO类，调度框架 

    AutofacExt:Ioc

    QuartzStartup:作业调度配置入口

    IService:服务的接口

    Service:服务的

    RequestDTO:DTO

    ReponseDTO:DTO

    Jobs:调度的作业


* Gzh.Template.Core.Infrastructure
基础设施，api响应数据格式

    ApiResponse:统一的api响应格式


* Gzh.Template.Core.Repository （领域层）
应用程序的仓储模块，基于mongodb和mysql的数据操作模型

    Core:领域类的核心基础，区分不同数据库

    DatabaseContext:数据库的上下文类，参考EF 

    Domain:领域类(实体类)

    Interface:数据基础操作接口

    BaseRepository... :数据基础操作类

    DBSetting:mongodb数据库配置
    

* Gzh.Template.Core.WebApi
应用程序的对外提供的restful风格api

    Controllers:控制器

    Startup:程序启动配置项