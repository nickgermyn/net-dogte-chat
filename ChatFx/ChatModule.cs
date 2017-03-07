using ChatFx.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ChatFx
{
    /// <summary>
    /// Basic class containing the functionality for defining routes and actions.
    /// </summary>
    public abstract class ChatModule : IChatModule
    {
        private readonly List<Route> routes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatModule"/> class.
        /// </summary>
        protected ChatModule()
        {
            this.routes = new List<Route>();
        }

        /// <summary>
        /// Declares a route that this module will handle
        /// </summary>
        /// <param name="path">The path that the route will respond to</param>
        /// <param name="action">Action that will be invoked when the route is hit</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Handle(string path, Func<string, Task> action, Func<ChatContext, bool> condition = null, string name = null)
        {
            this.Handle(path, (args, ct) => action(args), condition, name);
        }

        /// <summary>
        /// Declares a route that this module will handle
        /// </summary>
        /// <param name="path">The path that the route will respond to</param>
        /// <param name="action">Action that will be invoked when the route is hit</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        public virtual void Handle(string path, Func<string, CancellationToken, Task> action, Func<ChatContext, bool> condition = null, string name = null)
        {
            this.AddRoute(path, action, condition, name);
        }

        /// <summary>
        /// Gets all declared routes by the module.
        /// </summary>
        /// <value>A <see cref="IEnumerable{T}"/> instance, containing all <see cref="Route"/> instances declared by the module.</value>
        /// <remarks>This is automatically set by Nancy at runtime.</remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual IEnumerable<Route> Routes
        {
            get { return this.routes.AsReadOnly(); }
        }

        /// <summary>
        /// Gets or sets an <see cref="Request"/> instance that represents the current request.
        /// </summary>
        public virtual Request Request
        {
            get { return this.Context.Request; }
            set { this.Context.Request = value; }
        }

        /// <summary>
        /// Gets or sets the current Chat context
        /// </summary>
        /// <remarks>This is set automatically at runtime.</remarks>
        public ChatContext Context { get; set; }

        /// <summary>
        /// The telegram bot associated with the request
        /// </summary>
        /// <returns></returns>
        public ITelegramBotClient Bot => this.Request.Bot;

        /// <summary>
        /// Gets the message sender
        /// </summary>
        public User Sender => this.Request.Message.From;

        /// <summary>
        /// Gets the chat ID
        /// </summary>
        public long ChatId => this.Request.Message.Chat.Id;

        /// <summary>
        /// Responds to the Request with the given text message
        /// </summary>
        /// <param name="text">The text</param>
        /// <param name="parseMode">The parse mode</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        public async Task RespondTextMessageAsync(string text, ParseMode parseMode = ParseMode.Default, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Bot.SendTextMessageAsync(Request.Message.Chat.Id, text, parseMode: parseMode, cancellationToken: cancellationToken);
        }

        public async Task<Message> OnReplyToMessage(Message sent, TimeSpan timeout, CancellationToken cancellationToken)
        {
            //TODO: Support timeout via cancellation
            var reply = await Context.OnReplyToMessage(sent, cancellationToken);
            return reply;
        }

        /// <summary>
        /// Declares a route for the module
        /// </summary>
        /// <param name="path">The path that the route will respond to</param>
        /// <param name="action">Action that will be invoked when the route is hit</param>
        /// <param name="condition">A condition to determine if the route can be hit</param>
        /// <param name="name">Name of the route</param>
        private void AddRoute(string path, Func<string, CancellationToken, Task> action, Func<ChatContext, bool> condition, string name)
        {
            this.routes.Add(new Route(name ?? string.Empty, this.GetFullPath(path), condition, action));
        }

        private string GetFullPath(string path)
        {
            var relativePath = (path ?? string.Empty).Trim('/');
            return string.Concat("/", relativePath);
        }
    }
}