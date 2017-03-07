using ChatFx.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ChatFx
{
    /// <summary>
    /// Chat context
    /// </summary>
    public sealed class ChatContext : IDisposable
    {
        private Request request;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatContext"/> class.
        /// </summary>
        public ChatContext()
        {
            this.Items = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets the dictionary for storage of per-request items. Disposable items will be disposed when the context is.
        /// </summary>
        public IDictionary<string, object> Items { get; private set; }

        /// <summary>
        /// Gets or sets the resolved route
        /// </summary>
        public Route ResolvedRoute { get; set; }

        /// <summary>
        /// Gets or sets the incoming request
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        /// Gets or sets the request parameters
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// Disposes any disposable items in the <see cref="Items"/> dictionary.
        /// </summary>
        public void Dispose()
        {
            foreach (var disposableItem in this.Items.Values.OfType<IDisposable>())
            {
                disposableItem.Dispose();
            }

            this.Items.Clear();

            if (this.request != null)
            {
                ((IDisposable)this.request).Dispose();
            }
        }

        internal Task<Message> OnReplyToMessage(Message sent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}