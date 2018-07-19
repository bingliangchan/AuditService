using App.Audit.Infrastructure;
using App.Common;
using Autofac;

namespace App.Audit.QueryProcessor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BuildContainer().Resolve<App>().Run();
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<CommonModule>();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterType<App>();
            return builder.Build();
        }
    }
}