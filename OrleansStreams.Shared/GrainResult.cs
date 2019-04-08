using System;

namespace OrleansStreams.Shared
{
    public class GrainResult<T>
    {
        public GrainResult(Guid correlationId, T data)
        {
            CorrelationId = correlationId;
            Data = data;
        }

        public Guid CorrelationId { get; }

        public T Data { get; }
    }
}
