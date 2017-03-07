using System.Collections.Generic;

namespace ChatFx.Routing
{
    /// <summary>
    /// Contains a cache of all routes registered in the system
    /// </summary>
    public interface IRouteCache
    {
        /// <summary>
        /// The cached route collection
        /// </summary>
        IEnumerable<CachedRoute> CachedRoutes { get; }
    }
}