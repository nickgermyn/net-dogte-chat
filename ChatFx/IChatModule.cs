using ChatFx.Routing;
using System.Collections.Generic;

namespace ChatFx
{
    public interface IChatModule
    {
        /// <summary>
        /// Gets or sets an <see cref="Request"/> instance that represents the current request
        /// </summary>
        Request Request { get; set; }
        /// <summary>
        /// A collection of the modules routes
        /// </summary>
        IEnumerable<Route> Routes { get; }
        /// <summary>
        /// Request context
        /// </summary>
        ChatContext Context { get; set; }
    }
}