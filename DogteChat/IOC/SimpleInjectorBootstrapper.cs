using ChatFx.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using ChatFx.Routing;
using ChatFx;
using System.Reflection;

namespace DogteChat.IOC
{
    public class SimpleInjectorBootstrapper : ChatBootstrapper
    {
        private readonly Container container;

        public SimpleInjectorBootstrapper()
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            container.RegisterSingleton<IRouteCache, DefaultRouteCache>();
            container.RegisterSingleton<IRouteResolver, DefaultRouteResolver>();
            container.RegisterSingleton<IChatModuleCatalog>(this);
            container.Register<IRequestDispatcher, DefaultRequestDispatcher>();
            container.RegisterDecorator<IRequestDispatcher, ExecutionScopedRequestDispatcher>();
            container.RegisterSingleton<IChatContextFactory, DefaultChatContextFactory>();
            container.Register<IChatEngine, ChatEngine>();
            container.Register<IRequestExceptionHandler, DefaultRequestExceptionHandler>();

            var thisAssembly = typeof(SimpleInjectorBootstrapper).GetTypeInfo().Assembly;
            var moduleTypes = container.GetTypesToRegister(typeof(IChatModule), new[] { thisAssembly });
            container.RegisterCollection<IChatModule>(moduleTypes);
            foreach (var item in moduleTypes)
            {
                container.Register(item, item, Lifestyle.Scoped);
            }
        }

        public override IEnumerable<T> GetAllInstances<T>()
        {
            return container.GetAllInstances<T>();
        }

        public override T GetInstance<T>()
        {
            return container.GetInstance<T>();
        }

        public override object GetInstance(Type type)
        {
            return container.GetInstance(type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                container.Dispose();
            }
        }
    }
}
