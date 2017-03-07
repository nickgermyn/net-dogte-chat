using System;
using System.Collections.Generic;

namespace ChatFx.Bootstrapper
{
    public abstract class ChatBootstrapper : IChatBootstrapper, IChatModuleCatalog, IDisposable
    {
        private bool isDisposing;

        public ChatBootstrapper()
        {
        }

        public abstract T GetInstance<T>()
            where T : class;
        public abstract object GetInstance(Type type);
        public abstract IEnumerable<T> GetAllInstances<T>()
            where T : class;

        public IChatEngine GetEngine()
        {
            return GetInstance<IChatEngine>();
        }

        public void Initialise()
        {
            // Get all of the modules to extract their routes
        }

        public void Dispose()
        {
            // Prevent StackOverflowException if ApplicationContainer.Dispose re-triggers this Dispose
            if (this.isDisposing)
            {
                return;
            }

            isDisposing = true;

            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        public IEnumerable<IChatModule> GetAllModules()
        {
            return GetAllInstances<IChatModule>();
        }

        public IChatModule GetModule(Type moduleType)
        {
            var module = (IChatModule)GetInstance(moduleType);
            return module;
        }
    }
}
