using Autofac;
using PriceListConfigurator.Data.Repository;
using PriceListConfigurator.ViewModel;
using PriceListConfiguratorUI.View;

namespace PriceListConfiguratorUI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<SetupConnectionWindow>().AsSelf();
            builder.RegisterType<SetupConnectionViewModel>().AsSelf();

            builder.RegisterType<ConnectionRepository>().As<IConnectionRepository>().SingleInstance();

            return builder.Build();
        }
    }
}
