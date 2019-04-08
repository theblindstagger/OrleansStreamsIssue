using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansStreams.Shared;
using Owin;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrleansStreamsAPI.Console
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var provider = IocStartup.BuildServiceProvider();
            config.DependencyResolver = new DefaultDependencyResolver(provider);

            app.UseWebApi(config);
        }

        public static IClusterClient ConnectClient()
        {
            var clientBuilder = new ClientBuilder()
                .UseLocalhostClustering()
                .AddSimpleMessageStreamProvider(StreamConstants.ProviderName)
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasic";
                })
                .ConfigureLogging(logging => logging.AddConsole());

            var client = clientBuilder.Build();
            client.Connect().Wait();

            return client;
        }
    }
}
