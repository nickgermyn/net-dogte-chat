using System;
using System.Diagnostics;
using System.Text;

namespace ChatFx.Routing
{
    [DebuggerDisplay("{DebuggerDisplay, nq}")]
    public sealed class RouteDescription
    {
        public RouteDescription(string name, string path, Func<ChatContext, bool> condition)
        {
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path), "Path must be specified");
            }

            this.Name = name ?? string.Empty;
            this.Path = path;
            this.Condition = condition;
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
        public Func<ChatContext, bool> Condition { get; private set; }

        private string DebuggerDisplay
        {
            get
            {
                var builder = new StringBuilder();

                if (!string.IsNullOrEmpty(this.Name))
                {
                    builder.AppendFormat("{0} - ", this.Name);
                }

                builder.AppendFormat("{1}", this.Path);

                return builder.ToString();
            }
        }
    }
}