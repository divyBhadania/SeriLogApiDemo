using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System.Text;

namespace SeriLogApiDemo.Sink
{
    public class GoogleChatSink : IBatchedLogEventSink
    {

        public async Task EmitBatchAsync(IEnumerable<LogEvent> batch)
        {
            var cardsList = new List<object>();
            foreach (var item in batch)
            {
                cardsList.Add(new
                {
                    header = new
                    {
                        title = item.RenderMessage(),
                        subtitle = item.Timestamp,
                        imageUrl = "https://cdn0.iconfinder.com/data/icons/shift-free/32/Error-512.png"
                    }
                });
            }
            using (var client = new HttpClient())
            {
                var link = "https://chat.googleapis.com/v1/spaces/AAAAzrVkBs4/messages?key=AIzaSyDdI0hCZtE6vySjMm-WEfRq3CPzqKqqsHI&token=D-FWIVcdOr32EudoGhC9MhSHkyNzakjHsS7ndXWk-qY";
                var message = new
                {
                    text = "errors",
                    cards = cardsList
                };
                string jsonMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                await client.PostAsync(link, content);
            }
        }

        public Task OnEmptyBatchAsync()
        {
            return Task.FromResult(false);
        }
    }
}
