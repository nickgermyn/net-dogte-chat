using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace ChatFx.Routing
{
    public interface IRequestExceptionHandler
    {
        Task HandleError(Exception ex, ChatContext context, CancellationToken cancellationToken);
    }
}