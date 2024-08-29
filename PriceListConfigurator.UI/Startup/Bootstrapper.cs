using Autofac;

namespace PriceListConfiguratorUI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();

            return builder.Build();
        }
    }
}
