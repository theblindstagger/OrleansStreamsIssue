using Orleans;
using System.Threading.Tasks;

namespace OrleansStreams.GrainInterfaces
{
    public interface IInternalGrain : IGrainWithStringKey
    {
        Task ContinueProcess(int value);
    }
}