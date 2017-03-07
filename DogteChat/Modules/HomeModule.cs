using ChatFx;
using DogteChat.Data;
using DogteChat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DogteChat.Modules
{
    public class HomeModule : ChatModule
    {
        public HomeModule()
        {
            Handle("/echo", async (args, ct) =>
            {
                await Console.Out.WriteLineAsync("/echo " + args);
                await RespondTextMessageAsync("Echo: " + args, cancellationToken: ct);
            });
        }
    }

    public class AccountModule : ChatModule
    {
        private const string STEAM_ID = "steam";

        public AccountModule(IUserService userService)
        {
            Handle("/register", async (args, ct) =>
            {
                var regParams = ParseRegistrationArgs(args);

                // Find the user in the database
                var user = await userService.GetUserByTelegramId(Sender.Id);
                var fullName = string.Concat(Sender.FirstName, " " + Sender.LastName);

                if (user == null)
                {
                    // Create a new user
                    user = new User
                    {
                        TelegramId = Sender.Id,
                        UserName = Sender.Username,
                        FullName = fullName,
                        Steam32Id = regParams.Steam32Id,
                        SignupDate = DateTime.UtcNow
                    };

                    await userService.CreateUser(user);
                    await Bot.SendTextMessageAsync(ChatId, "Account created!", cancellationToken: ct);
                }
                else
                {
                    // Update existing user
                    user.UserName = Sender.Username;
                    user.FullName = fullName;
                    user.Steam32Id = regParams.Steam32Id;

                    await userService.UpdateUser(user);
                    await Bot.SendTextMessageAsync(ChatId, "Account updated!", cancellationToken: ct);
                }
            });

            Handle("/account", async (args, ct) =>
            {
                // Find the user in the database
                var user = await userService.GetUserByTelegramId(Sender.Id);
                if(user == null)
                {
                    await Bot.SendTextMessageAsync(ChatId, "Account not found! Have you registered?\n`/register steam:<steam64id>`", parseMode: ParseMode.Markdown, cancellationToken: ct);
                }
                else
                {
                    var userAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                    await Bot.SendTextMessageAsync(ChatId, $"`{userAsJson}`", parseMode: ParseMode.Markdown, cancellationToken: ct);
                }
            });

            Handle("/delete_account", async (args, ct) =>
            {
                // Find the user in the database
                var user = await userService.GetUserByTelegramId(Sender.Id);
                if (user == null)
                {
                    await Bot.SendTextMessageAsync(ChatId, "Account not found!", cancellationToken: ct);
                }
                else
                {
                    // Send confirmation message
                    var sent = await Bot.SendTextMessageAsync(ChatId, "Are you sure you wish to delete your account? (yes/no)", replyMarkup: new ForceReply { Force = true, Selective = true }, cancellationToken: ct);
                    var reply = await OnReplyToMessage(sent, TimeSpan.FromSeconds(10), ct);
                    var userAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                    await Bot.SendTextMessageAsync(ChatId, $"`{userAsJson}`", parseMode: ParseMode.Markdown, cancellationToken: ct);
                }
            });
        }

        public static RegistrationParameters ParseRegistrationArgs(string args)
        {
            var pairs = args.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (pairs.Length == 0)
            {
                throw new ArgumentParseException("Steam account details missing. Correct format\n`/register steam:<steam64id>`");
            }

            int? steam32Id = null;

            foreach (var item in pairs)
            {
                var kv = item.Split(':');
                if (kv.Length != 2)
                {
                    throw new ArgumentParseException("Invalid parameter `" + kv + "`. Needs to be semicolon delimeted");
                }

                if (kv[0] == STEAM_ID)
                {
                    var strVal = kv[1];
                    long parsed;
                    var success = Int64.TryParse(strVal, out parsed);
                    if (!success)
                    {
                        throw new ArgumentParseException("Unable to convert specified steam ID `" + strVal + "` to 64 bit integer.\nPlease ensure it is your steam 64 ID");
                    }

                    // Convert the 64bit ID back to a 32bit ID (low 32bits)
                    steam32Id = (int)parsed;
                }
            }

            return new RegistrationParameters(steam32Id);
        }
    }

    public class ArgumentParseException : Exception
    {
        public ArgumentParseException(string message)
            : base(message)
        {

        }
    }

    public class RegistrationParameters
    {
        public RegistrationParameters(int? steam32Id)
        {
            this.Steam32Id = steam32Id;
        }
        public int? Steam32Id { get; private set; }
    }
}
