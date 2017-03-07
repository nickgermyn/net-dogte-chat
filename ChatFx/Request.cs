using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ChatFx
{
    public class Request
    {
        private readonly Message message;
        private readonly ITelegramBotClient bot;

        public Request(Message message, ITelegramBotClient bot)
        {
            this.message = message;
            this.bot = bot;
        }

        public string Path => this.message.Text;
        public Message Message => this.message;
        public ITelegramBotClient Bot => this.bot;
    }
}
