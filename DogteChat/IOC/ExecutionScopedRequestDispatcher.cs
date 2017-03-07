using ChatFx;
using SimpleInjector;
using ChatFx.Routing;
using System.Threading;
using System.Threading.Tasks;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace DogteChat.IOC
{
    public class ExecutionScopedRequestDispatcher : IRequestDispatcher
    {
        private readonly IRequestDispatcher inner;
        private Container container;

        public ExecutionScopedRequestDispatcher(IRequestDispatcher inner, Container container)
        {
            this.inner = inner;
            this.container = container;
        }
        public async Task Dispatch(ChatContext context, CancellationToken cancellationToken)
        {
            using (container.BeginExecutionContextScope())
            {
                await inner.Dispatch(context, cancellationToken);
            }
        }
    }
}
