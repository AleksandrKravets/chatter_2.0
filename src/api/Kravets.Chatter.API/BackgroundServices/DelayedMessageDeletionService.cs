using Kravets.Chatter.API.BackgroundServices.Models.DelayedMessageDeletion;
using Kravets.Chatter.API.Hubs;
using Kravets.Chatter.API.Hubs.Contracts;
using Kravets.Chatter.BLL.Contracts.Commands.Messages.Delete;
using Kravets.Chatter.BLL.Extensions;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.BackgroundServices
{
    public class DelayedMessageDeletionService : BackgroundServiceBase
    {
        private readonly ConcurrentDictionary<Guid, DelayedMessageDeletingTask> _cancelledDeletions = new ConcurrentDictionary<Guid, DelayedMessageDeletingTask>();
        private readonly Channel<DelayedMessageDeletingTask> _channel;
        private readonly IServiceProvider _serviceProvider;

        public DelayedMessageDeletionService(
            IHostApplicationLifetime hostApplicationLifetime,
            Channel<DelayedMessageDeletingTask> channel,
            IServiceProvider serviceProvider, 
            ILogger<DelayedMessageDeletionService> logger) : base(hostApplicationLifetime, logger)
        {
            _channel = channel;
            _serviceProvider = serviceProvider;
        }

        private async Task DeleteMessage(Guid taskId, long messageId, long userId, IMediator mediator, IHubContext<NotificationsHub, INotificationsHub>  hubContext, CancellationToken cancellationToken)
        {
            await Task.Delay(20000);

            var isCancelled = _cancelledDeletions.TryGetValue(taskId, out var _);

            if (isCancelled)
            {
                _cancelledDeletions.TryRemove(taskId, out var _);
            }
            else
            {
                var response = await mediator.Send(new DeleteMessageRequest(userId, messageId), cancellationToken);
                if (response.IsSuccess()) await hubContext.Clients.All.Delete(messageId);
            }
        }

        protected override async Task ExecuteAsyncTest(CancellationToken cancellationToken)
        {
            while (!_channel.Reader.Completion.IsCompleted)
            {
                var message = await _channel.Reader.ReadAsync();

                if (message.Type == ActionType.CancelDeletion)
                {
                    _cancelledDeletions.AddOrUpdate(message.TaskId, message, (id, data) => data);
                }
                else if (message.Type == ActionType.Delete)
                {
                    using var scope = _serviceProvider.CreateScope();

                    var mediator = _serviceProvider.GetRequiredService<IMediator>();
                    var hubContext = _serviceProvider.GetRequiredService<IHubContext<NotificationsHub, INotificationsHub>>();

                    await DeleteMessage(
                        message.TaskId, 
                        message.MessageId, 
                        message.UserId, 
                        mediator, 
                        hubContext, 
                        cancellationToken);
                }
            }
        }
    }
}