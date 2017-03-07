using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace ChatFx.Bootstrapper
{

    /// <summary>
    /// Bootstrapper for the Chat engine
    /// </summary>
    public interface IChatBootstrapper
    {
        /// <summary>
        /// Initialises the bootstrapper.
        /// </summary>
        void Initialise();

        /// <summary>
        /// Gets the configured <see cref="IChatEngine"/>.
        /// </summary>
        /// <returns>A configured <see cref="IChatEngine"/> instance.</returns>
        /// <remarks>The bootstrapper must be initialised (see <see cref="Initialise"/>) prior to calling this</remarks>
        IChatEngine GetEngine();
    }
}
