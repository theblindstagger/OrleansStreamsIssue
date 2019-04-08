using Microsoft.Extensions.DependencyInjection;
using Orleans;
using OrleansStreamsAPI.Console;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

public static class IocStartup
{
    public static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
        // Register all dependent services
        // 
        // IocSomeAssembly.Register(services);    
        // 
        // services.AddTransient<ISomething, Something>()

        // For WebApi controllers, you may want to have a bit of reflection
        var controllerTypes = 
            Assembly.GetExecutingAssembly().GetExportedTypes()
                  .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                  .Where(t => typeof(ApiController).IsAssignableFrom(t)
            || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));

        foreach (var type in controllerTypes)
        {
            services.AddTransient(type);
        }

        var clusterClient = Startup.ConnectClient();

        services.AddSingleton<IClusterClient>(clusterClient);

        // It is only that you need to get service provider in the end
        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}