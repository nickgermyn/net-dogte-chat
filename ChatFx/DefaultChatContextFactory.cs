namespace ChatFx
{
    /// <summary>
    /// Creates ChatContext instances
    /// </summary>
    public class DefaultChatContextFactory : IChatContextFactory
    {
        public ChatContext Create(Request request)
        {
            var context = new ChatContext();
            context.Request = request;
            return context;
        }
    }
}