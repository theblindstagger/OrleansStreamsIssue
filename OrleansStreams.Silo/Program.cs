﻿using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansStreams.Grains;
using OrleansStreams.Shared;
using System;
using System.Threading.Tasks;

namespace OrleansStreams.Silo
{
    class Program
    {
        public static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("\n\n Press Enter to terminate...\n\n");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .AddSimpleMessageStreamProvider(StreamConstants.ProviderName)
                .AddMemoryGrainStorage(StreamConstants.PubSubStoreName)
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(PingGrain).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole())
                .EnableDirectClient();

            var host = builder.Build();

            await host.StartAsync();

            return host;
        }
    }
}