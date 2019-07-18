using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Gzh.Template.Core.Application.IService;
using Gzh.Template.Core.Application.Service;
using Gzh.Template.Core.Repository;
using Gzh.Template.Core.Repository.DatabaseContext;
using Gzh.Template.Core.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Gzh.Template.Core.Application
{
    /// <summary>
    /// Autofac IOC
    /// </summary>
    public static class AutofacExt
    {
        private static IContainer _container;
        public static IContainer InitAutofac(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            //注册数据库基础操作
            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepositoryMongoDB<>));
            builder.RegisterType<MongoDBContext>();
            builder.RegisterType<MysqlContext>();
            services.AddScoped(typeof(BaseRepositoryMongoDB<>));
            services.AddScoped(typeof(BaseRepositoryMysql<>));

            services.AddScoped(typeof(BaseRepositoryMongoDBByLinq));
            services.AddScoped(typeof(BaseRepositoryMysqlByLinq));




            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<BookService>().As<IBookService>();
            builder.RegisterType<HelloJobService>().As<IHelloJobService>();

            //注册app层
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            builder.Populate(services);

            _container = builder.Build();
            return _container;

        }
    }
}
