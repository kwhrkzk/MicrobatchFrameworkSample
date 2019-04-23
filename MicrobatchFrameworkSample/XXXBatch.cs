using MicroBatchFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.PowerShell.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicrobatchFrameworkSample
{
    public class XXXBatch : BatchBase
    {
        private ILogger<XXXBatch> Logger { get; }
        private Microsoft.Extensions.Hosting.IApplicationLifetime AppLifetime { get; }
        public XXXBatch(
            ILogger<XXXBatch> _logger, 
            Microsoft.Extensions.Hosting.IApplicationLifetime _appLifetime,
            IHostLifetime _hostLifetime,
            HostBuilderContext _context,
            IConfiguration _configuration,
            Microsoft.Extensions.Hosting.IHostingEnvironment _hostingEnvironment
            )
        {
            Logger = _logger;
            AppLifetime = _appLifetime;

            AppLifetime.ApplicationStarted.Register(OnStarted);
            AppLifetime.ApplicationStopping.Register(OnStopping);
            AppLifetime.ApplicationStopped.Register(OnStopped);
        }

        private void OnStarted()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public async Task Exexute()
        {
            if (Context.CancellationToken.IsCancellationRequested)
            {
                Environment.ExitCode = 0;
                return;
            }

            var waitSeconds = 10;
            Console.WriteLine(waitSeconds + " seconds");
            while ((waitSeconds != 0) && (Context.CancellationToken.IsCancellationRequested == false))
            {
                await Task.Delay(TimeSpan.FromSeconds(1), Context.CancellationToken);
                waitSeconds--;
                Console.WriteLine(waitSeconds + " seconds");
            }

            Environment.ExitCode = 0;
        }

        public enum MyEnum
        {
            Enum1,
            Enum2
        }

        public void BoolParam([Option("x", "説明")]bool x)
        {
            Console.WriteLine(x.ToString());
            Environment.ExitCode = 0;
        }

        public void EnumParam([Option("x", "説明")]MyEnum x)
        {
            Console.WriteLine(Enum.GetName(typeof(MyEnum), x));
            Environment.ExitCode = 0;
        }

        public void ListStringParam([Option("x", "説明")]List<string> x)
        {
            x.ForEach(item => Console.WriteLine(item));
            Environment.ExitCode = 0;
        }

        public void ListDoubleParam([Option("x", "説明")]List<double> x)
        {
            x.ForEach(item => Console.WriteLine(item));
            Environment.ExitCode = 0;
        }

        public void ListIntParam([Option("x", "説明")]List<int> x)
        {
            x.ForEach(item => Console.WriteLine(item));
            Environment.ExitCode = 0;
        }

        private void OnStopping()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        private void OnStopped()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }

    public class YYYHostedService : IHostedService
    {
        private ILogger<YYYHostedService> Logger { get; }
        private Microsoft.Extensions.Hosting.IApplicationLifetime AppLifetime { get; }
        public YYYHostedService(ILogger<YYYHostedService> _logger, Microsoft.Extensions.Hosting.IApplicationLifetime _appLifetime)
        {
            Logger = _logger;
            AppLifetime = _appLifetime;

            AppLifetime.ApplicationStarted.Register(OnStarted);
            AppLifetime.ApplicationStopping.Register(OnStopping);
            AppLifetime.ApplicationStopped.Register(OnStopped);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return Task.Run(() => {
            });
        }

        private void OnStarted()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return Task.Run(() => {
            });
        }

        private void OnStopping()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        private void OnStopped()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }

    public class XXXHostedService : IHostedService
    {
        private ILogger<XXXHostedService> Logger { get; }
        private Microsoft.Extensions.Hosting.IApplicationLifetime AppLifetime { get; }
        public XXXHostedService(ILogger<XXXHostedService> _logger, Microsoft.Extensions.Hosting.IApplicationLifetime _appLifetime)
        {
            Logger = _logger;
            AppLifetime = _appLifetime;

            AppLifetime.ApplicationStarted.Register(OnStarted);
            AppLifetime.ApplicationStopping.Register(OnStopping);
            AppLifetime.ApplicationStopped.Register(OnStopped);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return Task.Run(() => {
            });
        }

        private void OnStarted()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return Task.Run(() => {
            });
        }

        private void OnStopping()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        private void OnStopped()
        {
            Logger.LogInformation(this.GetType().Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}
