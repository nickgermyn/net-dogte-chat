using System;
using System.Collections.Generic;

namespace ChatFx
{
    /// <summary>
    /// Catalog of <see cref="IChatModule"/> instances.
    /// </summary>
    public interface IChatModuleCatalog
    {
        /// <summary>
        /// Get all ChatModule implementation instances - should be per-request lifetime
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> instance containing <see cref="IChatModule"/> instances.</returns>
        IEnumerable<IChatModule> GetAllModules();

        /// <summary>
        /// Retrieves a specific <see cref="IChatModule"/> implementation - should be per-request lifetime
        /// </summary>
        /// <param name="moduleType">Module type</param>
        /// <returns>The <see cref="IChatModule"/> instance</returns>
        IChatModule GetModule(Type moduleType);
    }
}