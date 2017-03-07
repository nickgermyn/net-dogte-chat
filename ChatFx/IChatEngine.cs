using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ChatFx
{
    /// <summary>
    /// Defines functionality of an engine that can handle Chat <see cref="Request"/>s.
    /// </summary>
    public interface IChatEngine : IDisposable
    {
        /// <summary>
        /// Handles an incoming <see cref="Request"/> async.
        /// </summary>
        /// <param name="request">An <see cref="Request"/> instance, containing the information about the current request.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task that represents the async operation</returns>
        Task<ChatContext> HandleRequest(Request request, CancellationToken cancellationToken);

        /// <summary>
        /// Handles an incoming <see cref="Request"/> async.
        /// </summary>
        /// <param name="request">An <see cref="Request"/> instance, containing the information about the current request.</param>
        /// <returns>Task that represents the async operation</returns>
        Task<ChatContext> HandleRequest(Request request);

        Task HandleReply(Message message);
    }
}