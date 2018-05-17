using PatchKit.Core.DependencyInjection;

namespace PatchKit.Network.Properties
{
    public class AssemblyModule
    {
        static AssemblyModule()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.OverwriteBindings(Core.Properties.AssemblyModule.Container.Bindings);

            containerBuilder.OverwriteBinding<IHttpClient, HttpClient>();
            containerBuilder.OverwriteBinding<IHttpDownloader, HttpDownloader>(1024);

            Container = containerBuilder.Container;
        }

        public static Container Container { get; }
    }
}