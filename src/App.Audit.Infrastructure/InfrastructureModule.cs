using App.Common.Validator;
using Autofac;
using System;
using App.Audit.Infrastructure.Repository;
using App.Common.Interface;
using App.Common.Utility;

namespace App.Audit.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(IMessageValidator<>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces();

            builder.RegisterType<AuditRepository>().As<IAuditRepository>();
            builder.RegisterType<MysqlConnector>().As<IMysqlConnector>();
            
        }
    }
}