using Orleans;
using System.Threading.Tasks;

namespace OrleansStreams.GrainInterfaces
{
    public interface IWorkerGrain : IGrainWithStringKey
    {
        Task CompleteProcess(int value);
    }
}
