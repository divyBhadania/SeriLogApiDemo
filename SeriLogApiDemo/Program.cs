using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Configuration;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

//Serilog Config
Serilog.Debugging.SelfLog.Enable(Console.Error);

//Appsettings.json configuration
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

//Code-based configuration(Inline)
//method 1
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

//method 2
//Log.Logger = new LoggerConfiguration()
//            .ReadFrom.Configuration(builder.Configuration)
//            .CreateLogger();
//builder.Services.AddLogging(loggingBuilder =>
//{
//    loggingBuilder.ClearProviders();
//    loggingBuilder.AddSerilog();
//});

//method 3
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

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
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
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

Log.CloseAndFlush();