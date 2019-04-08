using System.Threading.Tasks;
using Orleans;
using OrleansStreams.GrainInterfaces;

namespace OrleansStreams.Grains
{
    public class PingGrain : Grain, IPingGrain
    {
        public Task<string> Ping()
        {
            return Task.FromResult("Pong");
        }
    }
}
