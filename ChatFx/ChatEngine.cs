using ChatFx.Routing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ChatFx
{
    /// <summary>
    /// Default engine for handling Chat <see cref="Request"/>s.
    /// </summary>
    public class ChatEngine : IChatEngine
    {
        private readonly IRequestDispatcher dispatcher;
        private readonly IChatContextFactory contextFactory;
        private readonly CancellationTokenSource engineDisposedCts;
        private readonly ConcurrentDictionary<int, ChatContext> contexts = new ConcurrentDictionary<int, ChatContext>();

        /// <summary>
        /// Creates a new instance of the <see cref="ChatEngine"/> class.
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="contextFactory">A factory for creating contexts</param>
        public ChatEngine(
            IRequestDispatcher dispatcher,
            IChatContextFactory contextFactory)
        {
            this.dispatcher = dispatcher ?? throw new ArgumentNullException("dispatcher");
            this.contextFactory = contextFactory ?? throw new ArgumentNullException("contextFactory");
            this.engineDisposedCts = new CancellationTokenSource();
        }

        /// <summary>
        /// Handles an incoming <see cref="Request"/> async.
        /// </summary>
        /// <param name="request">An <see cref="Request"/> instance, containing the information about the current request.</param>
        /// <returns>Task that represents the async operation</returns>
        public Task<ChatContext> HandleRequest(Request request)
        {
            return HandleRequest(request, CancellationToken.None);
        }

        public Task HandleReply(Message message)
        {
            var messageId = message.ReplyToMessage.MessageId;
        }

        /// <summary>
        /// Handles an incoming <see cref="Request"/> async.
        /// </summary>
        /// <param name="request">An <see cref="Request"/> instance, containing the information about the current request.</param>
        /// <param name="preRequest">Delegate to call before the request is processed.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task that represents the async operation</returns>
        public async Task<ChatContext> HandleRequest(Request request, CancellationToken cancellationToken)
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(this.engineDisposedCts.Token, cancellationToken))
            {
                cts.Token.ThrowIfCancellationRequested();

                if(request == null)
                {
                    throw new ArgumentNullException("request", "The request parameter cannot be null.");
                }

                var context = this.contextFactory.Create(request);
                contexts.TryAdd(request.Message.MessageId, context);

                try
                {
                    await this.dispatcher.Dispatch(context, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    //TODO: Deal with this
                }

                return context;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            this.engineDisposedCts.Cancel();
        }
    }
}