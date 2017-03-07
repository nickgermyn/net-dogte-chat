using System;

namespace ChatFx.Routing
{
    /// <summary>
    /// Represents a cached route
    /// </summary>
    public class CachedRoute
    {
        /// <summary>
        /// The index of the route within the module
        /// </summary>
        public int RouteIndex { get; set; }
        /// <summary>
        /// The routes path
        /// </summary>
        public string RoutePath { get; set; }
        /// <summary>
        /// The module type
        /// </summary>
        public Type ModuleType { get; set; }
        /// <summary>
        /// The condition associated with the route (if any)
        /// </summary>
        public Func<ChatContext, bool> Condition { get; set; }
    }
}