using Orleans;
using Orleans.Streams;
using OrleansStreams.GrainInterfaces;
using OrleansStreams.Shared;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrleansStreamsAPI.Console.Controllers
{
    public class TestController : ApiController
    {
        private readonly IClusterClient clusterClient;
        private Guid correlationId;
        private GrainResult<string> result;

        public TestController(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient; // Startup.ConnectClient().GetAwaiter().GetResult();
            this.correlationId = Guid.NewGuid();

            Orleans.Runtime.RequestContext.Set(StreamConstants.CorrelationId, this.correlationId);
        }

        public async Task<string> Get(int id)
        {
            var provider = this.clusterClient.GetStreamProvider(StreamConstants.ProviderName);
            var stream = provider.GetStream<GrainResult<string>>(Guid.Empty, StreamConstants.StreamNamespace);

            var subscription = await stream.SubscribeAsync(async (result, token) =>
            {
                if (result.CorrelationId == this.correlationId)
                {
                    await System.Threading.Tasks.Task.Run(() => this.result = result);
                }
            });

            this.clusterClient.GetGrain<IBoundaryGrain>("id").InvokeOneWay(grain => grain.StartProcess(id));

            while (this.result == null)
            {
                await Task.Delay(200);
            }

            await subscription.UnsubscribeAsync();

            return this.result.Data;
        }
    }
}
