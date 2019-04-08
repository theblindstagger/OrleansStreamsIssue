using Orleans;
using OrleansStreams.GrainInterfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace OrleansStreamsAPI.Console.Controllers
{
    public class PingController : ApiController
    {
        private readonly IClusterClient clusterClient;

        public PingController(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient;
        }

        public async Task<string> Get()
        {
            return await this.clusterClient.GetGrain<IPingGrain>(0).Ping();
        }
    }
}
