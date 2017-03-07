using System.Threading;
using System.Threading.Tasks;

namespace ChatFx.Routing
{
    /// <summary>
    /// Functionality for processing an incoming request
    /// </summary>
    public interface IRequestDispatcher
    {
        /// <summary>
        /// Dispatches a request
        /// </summary>
        /// <param name="context">The <see cref="ChatContext"/> for the current request.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task Dispatch(ChatContext context, CancellationToken cancellationToken);
    }
}