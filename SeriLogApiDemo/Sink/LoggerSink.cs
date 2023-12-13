using Serilog.Core;
using Serilog.Events;

namespace SeriLogApiDemo.Sink
{
    public class LoggerSink : ILogEventSink
    {
        public void Emit(LogEvent logEvent)
        {
            Console.WriteLine(logEvent.Properties.FirstOrDefault(i => i.Key == "RequestIP").Value);
        }
    }
}
