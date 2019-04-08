using Orleans;
using System.Threading.Tasks;

namespace OrleansStreams.GrainInterfaces
{
    public interface IPingGrain : IGrainWithIntegerKey
    {
        Task<string> Ping();
    }
}
