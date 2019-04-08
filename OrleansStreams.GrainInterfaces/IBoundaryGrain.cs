using Orleans;
using System.Threading.Tasks;

namespace OrleansStreams.GrainInterfaces
{
    public interface IBoundaryGrain : IGrainWithStringKey
    {
        Task StartProcess(int value);
    }
}
