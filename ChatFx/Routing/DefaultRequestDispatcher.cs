using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatFx.Routing
{ 
    /// <summary>
    /// Default implementation of a request dispatcher.
    /// </summary>
    public class DefaultRequestDispatcher : IRequestDispatcher
    {
        private readonly IRouteResolver routeResolver;
        private readonly IRequestExceptionHandler exceptionHandler;

        /// <summary>
        /// Creates a new instance of the <see cref="DefaultRequestDispatcher"/> class.
        /// </summary>
        /// <param name="routeResolver">Route resolver instance.</param>
        /// <param name="exceptionHandler">The exception handler</param>
        public DefaultRequestDispatcher(
            IRouteResolver routeResolver,
            IRequestExceptionHandler exceptionHandler)
        {
            this.routeResolver = routeResolver;
            this.exceptionHandler = exceptionHandler;
        }

        /// <summary>
        /// Dispatches a request
        /// </summary>
        /// <param name="context">The <see cref="ChatContext"/> for the current request.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public async Task Dispatch(ChatContext context, CancellationToken cancellationToken)
        {
            var resolveResult = this.routeResolver.Resolve(context);

            context.Parameters = resolveResult.Parameters;
            context.ResolvedRoute = resolveResult.Route;

            try
            {
                await resolveResult.Route.Action(resolveResult.Parameters, cancellationToken).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                await this.exceptionHandler.HandleError(ex, context, cancellationToken);
            }
        }
    }
}