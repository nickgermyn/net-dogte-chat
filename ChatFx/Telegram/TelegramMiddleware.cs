using ChatFx.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using Telegram.Bot;

namespace ChatFx.Telegram
{
    public class TelegramMiddleware
    {
        public static IDisposable Use(ITelegramBotClient bot, IChatBootstrapper bootstrapper)
        {
            bootstrapper.Initialise();
            var engine = bootstrapper.GetEngine();

            var disp1 = bot.GetMessageReceivedObservable()
                .Where(msg => msg.Text != null)
                .Select(async msg =>
                {
                    var chatRequest = new Request(msg, bot);
                    var context = await engine.HandleRequest(chatRequest);
                    context.Dispose();
                })
                .Subscribe();

            var disp2 = bot.GetMessageReceivedObservable()
                .Where(msg => msg.ReplyToMessage != null)
                .Select(async msg =>
                {
                    await engine.HandleReply(msg);
                })
                .Subscribe();

            return new CompositeDisposable(disp1, disp2);
        }
    }
}
