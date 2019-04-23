using MicroBatchFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MicrobatchFrameworkSample
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            try
            {
                await BatchHost
                    .CreateDefaultBuilder()
#if DEBUG
                    .UseEnvironment(EnvironmentName.Development)
#else
                    .UseEnvironment(EnvironmentName.Production)
#endif
                    .ConfigureAppConfiguration(ConfigureAppConfiguration)
                    .ConfigureLogging(ConfigureLogging)
                    .ConfigureServices((context, services) =>
                    {
                        //services.AddHostedService<XXXHostedService>();
                        //services.AddHostedService<YYYHostedService>();
                    })
                    .RunBatchEngineAsync(args, new CompositeBatchInterceptor(new XXXInterceptor()));

                return Environment.ExitCode;
            }
            catch (Exception ex) when ((ex is ArgumentException) || (ex is ArgumentNullException))
            {
                return 1;
            }
            catch (Exception)
            {
                return 9;
            }
        }

        private static void ConfigureLogging(HostBuilderContext hostingContext, ILoggingBuilder logging)
        {
            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            logging.AddConsole();
            logging.AddDebug();
            logging.AddEventSourceLogger();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext hostContext, IConfigurationBuilder configApp)
        {
            configApp.SetBasePath(Directory.GetCurrentDirectory());
            configApp.AddJsonFile("appsettings.json", optional: true);
            configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
            configApp.AddEnvironmentVariables();
        }
    }

    public class XXXInterceptor : IBatchInterceptor
    {
        private ILogger<BatchEngine> Logger { get; set; }

        public ValueTask OnBatchEngineBeginAsync(IServiceProvider serviceProvider, ILogger<BatchEngine> logger)
        {
            Logger = logger;
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return default;
        }

        public ValueTask OnBatchEngineEndAsync()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return default;
        }

        public ValueTask OnBatchRunBeginAsync(BatchContext context)
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return default;
        }

        public ValueTask OnBatchRunCompleteAsync(BatchContext context, string errorMessageIfFailed, Exception exceptionIfExists)
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return default;
        }
    }
}
