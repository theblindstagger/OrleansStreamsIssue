using System.Threading.Tasks;
using Orleans;
using OrleansStreams.GrainInterfaces;

namespace OrleansStreams.Grains
{
    public class InternalGrain : Grain, IInternalGrain
    {
        public async Task ContinueProcess(int value)
        {
            await Task.Delay(300);

            this.GrainFactory.GetGrain<IWorkerGrain>("id").InvokeOneWay(grain => grain.CompleteProcess(value));
        }
    }
}
