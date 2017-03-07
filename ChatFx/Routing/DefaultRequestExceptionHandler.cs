using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace ChatFx.Routing
{
    public class DefaultRequestExceptionHandler : IRequestExceptionHandler
    {
        public async Task HandleError(Exception ex, ChatContext context, CancellationToken cancellationToken)
        {
            //TODO: Log the error
            var bot = context.Request.Bot;
            var text = ex.Message;
            await bot.SendTextMessageAsync(context.Request.Message.Chat.Id, text, parseMode: ParseMode.Markdown, cancellationToken: cancellationToken);
        }
    }
}