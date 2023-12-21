using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;

namespace SeriLogApiDemo.Sink
{
    public class BatchLoggerSink : IBatchedLogEventSink
    {
        public async Task EmitBatchAsync(IEnumerable<LogEvent> batch)
        {
            foreach (var item in batch)
            {
                Console.WriteLine(item.Properties.FirstOrDefault(i => i.Key == "RequestIP").Value);
            }
        }

        public Task OnEmptyBatchAsync()
        {
            return Task.FromResult(false);
        }
    }
}
