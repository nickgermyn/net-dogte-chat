using System;

namespace ChatFx
{
    /// <summary>
    /// Creates ChatContext instances.
    /// </summary>
    public interface IChatContextFactory
    {
        /// <summary>
        /// Creates a new ChatContext from the <see cref="Request"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ChatContext Create(Request request);
    }
}