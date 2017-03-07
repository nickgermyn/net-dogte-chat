using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChatFx.Routing
{
    /// <summary>
    /// Returns a route that matches the request
    /// </summary>
    public class DefaultRouteResolver : IRouteResolver
    {
        private readonly IChatModuleCatalog catalog;
        private readonly IRouteCache routeCache;

        public DefaultRouteResolver(
            IChatModuleCatalog catalog,
            IRouteCache routeCache)
        {
            this.catalog = catalog;
            this.routeCache = routeCache;
        }

        /// <summary>
        /// Gets the route, and the corresponding parameters from the message
        /// </summary>
        /// <param name="context">Current context</param>
        /// <returns>A <see cref="RouteResolutionResult"/> containing the resolved route information.</returns>
        public RouteResolutionResult Resolve(ChatContext context)
        {
            var requestPath = context.Request.Path;
            var matchResults = this.routeCache
                .CachedRoutes
                .Select(r => new
                {
                    ModuleType = r.ModuleType,
                    RouteIndex = r.RouteIndex,
                    Condition = r.Condition,
                    Match = CheckMatch(requestPath, r.RoutePath)
                })
                .Where(x => x.Match.Success)
                .Select(x => new MatchResult
                {
                    ModuleType = x.ModuleType,
                    RouteIndex = x.RouteIndex,
                    Parameters = ExtractParams(x.Match),
                    Condition = x.Condition
                })
                .ToList();

            for (int i = 0; i < matchResults.Count; i++)
            {
                var matchResult = matchResults[i];
                if(matchResult.Condition == null || matchResult.Condition.Invoke(context))
                {
                    return this.BuildResult(context, matchResult);
                }
            }

            return GetNotFoundResult(context);
        }

        private RouteResolutionResult BuildResult(ChatContext context, MatchResult result)
        {
            var associatedModule = this.GetModuleFromMatchResult(context, result);
            var route = associatedModule.Routes.ElementAt(result.RouteIndex);

            return new RouteResolutionResult(route, result.Parameters);
        }

        private IChatModule GetModuleFromMatchResult(ChatContext context, MatchResult result)
        {
            var module = this.catalog.GetModule(result.ModuleType);
            module.Context = context;
            return module;
        }

        private string ExtractParams(Match match)
        {
            if (!match.Success)
                return null;

            // We have found a match
            // Groups[0] = the full match
            // Groups[1] = the command name
            // Groups[2] = the args (if any)
            var args = match.Groups.Count >= 3 ? match.Groups[2].Value : null;
            return args;
        }

        private Match CheckMatch(string requestPath, string routePath)
        {
            var regex = @"(" + routePath + @")(?:@\w*)?(?:\s)?(.+)?";
            return Regex.Match(requestPath, regex, RegexOptions.IgnoreCase);
        }

        private static RouteResolutionResult GetNotFoundResult(ChatContext context)
        {
            return new RouteResolutionResult
            {
                Route = new NotFoundRoute(context.Request.Path),
                Parameters = string.Empty
            };
        }

        class MatchResult
        {
            public Type ModuleType { get; set; }
            public int RouteIndex { get; set; }
            public string Parameters { get; set; }
            public Func<ChatContext, bool> Condition { get; set; }
        }
    }
}