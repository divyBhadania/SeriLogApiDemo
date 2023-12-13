using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;

namespace SeriLogApiDemo.Sink
{
    public class BatchLoggerSink : IBatchedLogEventSink
    {
        public async Task EmitBatchAsync(IEnumerable<LogEvent> batch)
        {
            
        }

        public Task OnEmptyBatchAsync()
        {
            return Task.FromResult(false);
        }
    }
}
