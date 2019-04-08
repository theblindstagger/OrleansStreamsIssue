using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;
using Orleans.Streams;
using OrleansStreams.GrainInterfaces;
using OrleansStreams.Shared;

namespace OrleansStreams.Grains
{
    [StatelessWorker]
    public class WorkerGrain : Grain, IWorkerGrain
    {
        private IAsyncStream<GrainResult<string>> stream;

        public override async Task OnActivateAsync()
        {
            var provider = this.GetStreamProvider(StreamConstants.ProviderName);
            this.stream = provider.GetStream<GrainResult<string>>(Guid.Empty, StreamConstants.StreamNamespace);

            await base.OnActivateAsync();
        }

        public async Task CompleteProcess(int value)
        {
            await Task.Delay(300);

            var correlationId = Guid.Parse(RequestContext.Get(StreamConstants.CorrelationId).ToString());

            await this.stream.OnNextAsync(new GrainResult<string>(correlationId, $"My result: {value}"));
        }
    }
}
