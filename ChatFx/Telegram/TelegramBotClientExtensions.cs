using System;
using System.Linq;
using System.Reactive.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace ChatFx.Telegram
{
    public static class TelegramBotClientExtensions
    {
        public static IObservable<Message> GetMessageReceivedObservable(this ITelegramBotClient bot)
        {
            return Observable.FromEventPattern<MessageEventArgs>(
                h => bot.OnMessage += h,
                h => bot.OnMessage -= h)
                .Select(x => x.EventArgs.Message);
        }
    }
}