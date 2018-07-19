using System;
using App.Common.Dispatcher;
using App.Common.Executor;
using App.Common.Validator;
using Autofac;
using EasyNetQ;

namespace App.Common
{
    public class CommonModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //TODO: Change Buss Settings
            var bus = RabbitHutch.CreateBus("host=wolverine.rmq.cloudamqp.com;UserName=fulqjtps;Password=1XsWX9wiwup1UNo1hpR-lJYbjVK765hO;virtualhost=fulqjtps");
            builder.RegisterInstance(bus).SingleInstance();

            builder.RegisterType<Dispatcher.Dispatcher>().As<ICommandDispatcher>();
            builder.RegisterType<Dispatcher.Dispatcher>().As<IQueryDispatcher>();
            builder.RegisterType<Dispatcher.Dispatcher>().As<IEventDispatcher>();


            builder.RegisterType<Executor.Executor>().As<ICommandExecutor>();
            builder.RegisterType<Executor.Executor>().As<IEventExecutor>();
            builder.RegisterType<Executor.Executor>().As<IQueryExecutor>();



            builder.RegisterType<Validator.Validator>().As<IValidator>();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(IMessageValidator<>)).AsImplementedInterfaces();
        }
    }
}