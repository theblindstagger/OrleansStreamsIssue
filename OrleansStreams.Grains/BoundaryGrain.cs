using System.Threading.Tasks;
using Orleans;
using OrleansStreams.GrainInterfaces;

namespace OrleansStreams.Grains
{
    public class BoundaryGrain : Grain, IBoundaryGrain
    {
        public async Task StartProcess(int value)
        {
            await Task.Delay(100);

            this.GrainFactory.GetGrain<IInternalGrain>("id").InvokeOneWay(grain => grain.ContinueProcess(value));
        }
    }
}
