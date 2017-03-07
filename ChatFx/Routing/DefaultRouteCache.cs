using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatFx.Routing
{
    /// <summary>
    /// Contains a cache of all routes registered in the system
    /// </summary>
    public class DefaultRouteCache : IRouteCache
    {
        private readonly Lazy<IEnumerable<CachedRoute>> cache;
        private readonly IChatModuleCatalog moduleCatalog;

        /// <summary>
        /// Creates a new instance of the <see cref="DefaultRouteCache"/> class.
        /// </summary>
        /// <param name="moduleCatalog"></param>
        public DefaultRouteCache(IChatModuleCatalog moduleCatalog)
        {
            this.moduleCatalog = moduleCatalog;
            cache = new Lazy<IEnumerable<CachedRoute>>(() => LoadRoutes());
        }

        private IEnumerable<CachedRoute> LoadRoutes()
        {
            return moduleCatalog
                .GetAllModules()
                .SelectMany(m => m.Routes.Select((r, i) => new CachedRoute
                {
                    RouteIndex = i,
                    RoutePath = r.Description.Path,
                    ModuleType = m.GetType(),
                    Condition = r.Description.Condition
                }));
        }

        public IEnumerable<CachedRoute> CachedRoutes => cache.Value;
    }
}