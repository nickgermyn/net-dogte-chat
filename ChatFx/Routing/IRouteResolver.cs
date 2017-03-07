namespace ChatFx.Routing
{
    /// <summary>
    /// Returns a route that matches the request
    /// </summary>
    public interface IRouteResolver
    {
        /// <summary>
        /// Gets the route, and the corresponding parameters from the message
        /// </summary>
        /// <param name="context">Current context</param>
        /// <returns>A <see cref="RouteResolutionResult"/> containing the resolved route information.</returns>
        RouteResolutionResult Resolve(ChatContext context);
    }
}