namespace ChatFx.Routing
{
    /// <summary>
    /// A class representing a route resolution result
    /// </summary>
    public class RouteResolutionResult
    {
        /// <summary>
        /// Gets or sets the route
        /// </summary>
        public Route Route { get; set; }
        /// <summary>
        /// Gets or sets the captured parameters
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteResolutionResult"/> class.
        /// </summary>
        public RouteResolutionResult()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteResolutionResult"/> class with
        /// the provided <paramref name="route"/> and <paramref name="parameters"/>,
        /// </summary>
        /// <param name="route">The request route instance.</param>
        /// <param name="parameters">The parameters.</param>
        public RouteResolutionResult(Route route, string parameters)
        {
            this.Route = route;
            this.Parameters = parameters;
        }
    }
}