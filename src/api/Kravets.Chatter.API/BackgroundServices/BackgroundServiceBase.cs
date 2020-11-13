using Kravets.Chatter.Common.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.BackgroundServices
{
    public abstract class BackgroundServiceBase : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<BackgroundServiceBase> _logger;

        public BackgroundServiceBase(
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<BackgroundServiceBase> logger)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
        }

        protected abstract Task ExecuteAsyncTest(CancellationToken cancellationToken);

        protected override Task ExecuteAsync(CancellationToken cancellationToken) => Task.Run(async () =>
        {
            try
            {
                await ExecuteAsyncTest(cancellationToken);
            }
            catch (Exception ex) when (False(() => EventLog.BackgroundCritical(_logger, ex.Message, ex)))
            {
                throw;
            }
            finally
            {
                _hostApplicationLifetime.StopApplication();
            }
        });

        private static bool False(Action action) { action(); return false; }
    }
}