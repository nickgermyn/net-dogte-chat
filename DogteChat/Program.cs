using ChatFx.Telegram;
using DogteChat.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace DogteChat
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("APIKEY");

        static void Main(string[] args)
        {
            Console.WriteLine("Configuring bootstrapper...");
            var bootstrapper = new SimpleInjectorBootstrapper();
            Console.WriteLine("Setting up subscriptions...");
            var subscription = TelegramMiddleware.Use(Bot, bootstrapper);
            Console.WriteLine("Subscription created!");

            Console.WriteLine("Getting username...");
            var me = Bot.GetMeAsync().Result;

            Console.Title = me.Username;

            Bot.StartReceiving();
            Console.WriteLine("Bot receiving!");
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
            Bot.StopReceiving();
            subscription.Dispose();
        }

        //private static void Bot_OnInlineResultChosen(object sender, Telegram.Bot.Args.ChosenInlineResultEventArgs e)
        //{
        //    Console.WriteLine($"Received choosen inline result: {e.ChosenInlineResult.ResultId}");
        //}

        //private static async void Bot_OnInlineQuery(object sender, Telegram.Bot.Args.InlineQueryEventArgs e)
        //{
        //    InlineQueryResult[] results = {
        //        new InlineQueryResultLocation
        //        {
        //            Id = "1",
        //            Latitude = 40.7058316f, // displayed result
        //            Longitude = -74.2581888f,
        //            Title = "New York",
        //            InputMessageContent = new InputLocationMessageContent // message if result is selected
        //            {
        //                Latitude = 40.7058316f,
        //                Longitude = -74.2581888f,
        //            }
        //        },

        //        new InlineQueryResultLocation
        //        {
        //            Id = "2",
        //            Longitude = 52.507629f, // displayed result
        //            Latitude = 13.1449577f,
        //            Title = "Berlin",
        //            InputMessageContent = new InputLocationMessageContent // message if result is selected
        //            {
        //                Longitude = 52.507629f,
        //                Latitude = 13.1449577f
        //            }
        //        }
        //    };

        //    await Bot.AnswerInlineQueryAsync(e.InlineQuery.Id, results, isPersonal: true, cacheTime: 0);
        //}

        //private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        //{
        //    var message = e.Message;

        //    // Only accept test messages presently
        //    if (message == null || message.Type != Telegram.Bot.Types.Enums.MessageType.TextMessage) return;

        //    await messageDispatcher.Dispatch(e.Message);

        //    if (message.Text.StartsWith("/inline"))
        //    {
        //        await Bot.SendChatActionAsync(message.Chat.Id, Telegram.Bot.Types.Enums.ChatAction.Typing);

        //        var keyboard = new InlineKeyboardMarkup(new[]
        //        {
        //            new[]   // First row
        //            {
        //                new InlineKeyboardButton("1.1"),
        //                new InlineKeyboardButton("1.2"),
        //            },
        //            new[]   // Second row
        //            {
        //                new InlineKeyboardButton("2.1"),
        //                new InlineKeyboardButton("2.2"),
        //            }
        //        });

        //        await Task.Delay(500);  // Simulate long running task

        //        await Bot.SendTextMessageAsync(message.Chat.Id, "Choose", replyMarkup: keyboard);
        //    }

        //    else if (message.Text.StartsWith("/keyboard")) // send custom keyboard
        //    {
        //        var keyboard = new ReplyKeyboardMarkup(new[]
        //        {
        //            new [] // first row
        //            {
        //                new KeyboardButton("1.1"),
        //                new KeyboardButton("1.2"),
        //            },
        //            new [] // last row
        //            {
        //                new KeyboardButton("2.1"),
        //                new KeyboardButton("2.2"),
        //            }
        //        });

        //        await Bot.SendTextMessageAsync(message.Chat.Id, "Choose",
        //            replyMarkup: keyboard);
        //    }

        //    else if (message.Text.StartsWith("/request")) // request location or contact
        //    {
        //        var keyboard = new ReplyKeyboardMarkup(new[]
        //        {
        //            new KeyboardButton("Location")
        //            {
        //                RequestLocation = true
        //            },
        //            new KeyboardButton("Contact")
        //            {
        //                RequestContact = true
        //            },
        //        });

        //        await Bot.SendTextMessageAsync(message.Chat.Id, "Who or Where are you?", replyMarkup: keyboard);
        //    }

        //    else
        //    {
        //        await Bot.SendTextMessageAsync(message.Chat.Id, "Echo: " + message.Text);
        //    }
        //}
    }
}