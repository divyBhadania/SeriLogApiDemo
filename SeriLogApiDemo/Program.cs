using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Serilog;
using Serilog.Sinks.Discord;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.PeriodicBatching;
using SeriLogApiDemo.Middleware;
using SeriLogApiDemo.Sink;
using System.Configuration;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

//Serilog Config
Serilog.Debugging.SelfLog.Enable(Console.Error);

//Appsettings.json configuration
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

#region custom sink
//builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)
//.WriteTo.Sink(new LoggerSink(), restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
//.WriteTo.Sink(new PeriodicBatchingSink(new BatchLoggerSink(), new PeriodicBatchingSinkOptions
//{
//    BatchSizeLimit = 100,
//    Period = TimeSpan.FromSeconds(10)
//})
// , restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
#region Google chat
//.WriteTo.Sink(new PeriodicBatchingSink(new GoogleChatSink(), new PeriodicBatchingSinkOptions
//{
//    BatchSizeLimit = 100,
//    Period = TimeSpan.FromSeconds(10),
//    EagerlyEmitFirstEvent = false,  // set default to false, not usable for emailing
//    QueueLimit = 10000
//}))
#endregion
#region Discord sink
//.WriteTo.Discord(1186531800831492186, "i80qrkx8U6iGG0oCyGWdPXVqqH7CXD86bEKszDVRthkgLtn0H8FqJqxy2C7YhebGRfAn", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
#endregion
//);
#endregion

//Code-based configuration(Inline)
#region method1
//builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext} {Message}{NewLine}{Exception}")
//                                       .Enrich.FromLogContext()
//                                       .WriteTo.File(path: "Logs/log.txt"
//                                                     , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] [{Action}] {Message}{NewLine}{Exception}"
//                                                     , rollingInterval: RollingInterval.Day
//                                                     , flushToDiskInterval: TimeSpan.FromSeconds(5))
//                                       .WriteTo.MSSqlServer(connectionString: "Server = DESKTOP-AETEI14\\SQLEXPRESS; Database = SeriLog;Trusted_Connection = true; Encrypt = False"
//                                                            , sinkOptions: new MSSqlServerSinkOptions()
//                                                            {
//                                                                AutoCreateSqlTable = false,
//                                                                TableName = "seriLog",
//                                                                BatchPostingLimit = 100,
//                                                                BatchPeriod = TimeSpan.FromSeconds(10.0),
//                                                            }
//                                                            , restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error));
#endregion

#region method2
//method 2
//Log.Logger = new LoggerConfiguration()
//            .ReadFrom.Configuration(builder.Configuration)
//            .CreateLogger();
//builder.Services.AddLogging(loggingBuilder =>
//{
//    loggingBuilder.ClearProviders();
//    loggingBuilder.AddSerilog();
//});
#endregion

#region method3
//Log.Logger = new LoggerConfiguration()
//            .Enrich.FromLogContext()
//            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext} {Message}{NewLine}{Exception}")
//            .WriteTo.File(path: "Logs/log.txt"
//                           , outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] [{Action}] {Message}{NewLine}{Exception}"
//                           , rollingInterval: RollingInterval.Day
//                           , flushToDiskInterval: TimeSpan.FromSeconds(5))
//            .WriteTo.MSSqlServer(connectionString: "Server = DESKTOP-AETEI14\\SQLEXPRESS; Database = SeriLog;Trusted_Connection = true; Encrypt = False"
//                                , sinkOptions: new MSSqlServerSinkOptions()
//                                {
//                                    AutoCreateSqlTable = false,
//                                    TableName = "seriLog",
//                                    BatchPostingLimit = 100,
//                                    BatchPeriod = TimeSpan.FromSeconds(10.0),
//                                }
//                                , restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
//            .CreateLogger();
//builder.Services.AddLogging(loggingBuilder =>
//{
//    loggingBuilder.ClearProviders();
//    loggingBuilder.AddSerilog();
//});
#endregion

#region azure ad auth
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
#endregion

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();  // Only if we are using builder.Host method.
app.UseUserLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

Log.CloseAndFlush();