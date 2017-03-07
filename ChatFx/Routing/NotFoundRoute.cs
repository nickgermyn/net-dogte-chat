using System.Threading.Tasks;

namespace ChatFx.Routing
{
    public class NotFoundRoute : Route
    {
        public NotFoundRoute(string path)
            : base(string.Empty, path, null, (x,c) => Task.FromResult(default(object)))
        {

        }
    }
}