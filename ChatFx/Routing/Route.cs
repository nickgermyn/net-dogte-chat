using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatFx.Routing
{
    /// <summary>
    /// Defines the core functionality of a route
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Route"/> type, with the specified <see cref="RouteDescription"/>.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="action">The action that should take place when the route is invoked</param>
        public Route(RouteDescription description, Func<string, CancellationToken, Task> action)
        {
            this.Description = description;
            this.Action = action;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Route"/> type, with the specified definiiton.
        /// </summary>
        /// <param name="name">Route name</param>
        /// <param name="path">The path that the reoute is declared for.</param>
        /// <param name="condition">A condition that needs to the satisified in order for hte route to be eligible for invocation</param>
        /// <param name="action">The action that should take place when the route is invoked</param>
        public Route(string name, string path, Func<ChatContext, bool> condition, Func<string, CancellationToken, Task> action)
            : this(new RouteDescription(name, path, condition), action)
        {

        }

        /// <summary>
        /// Gets the description of the route
        /// </summary>
        public RouteDescription Description { get; private set; }

        /// <summary>
        /// Gets the action that should take place when the route is invoked
        /// </summary>
        public Func<string, CancellationToken, Task> Action { get; private set; }

        /// <summary>
        /// Invokes the route with the provided <paramref name="parameters"/>
        /// </summary>
        /// <param name="parameters">A string that contains the parameters that should be passed to the route</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public async Task Invoke(string parameters, CancellationToken cancellationToken)
        {
            await this.Action.Invoke(parameters, cancellationToken).ConfigureAwait(false);
        }
    }
}