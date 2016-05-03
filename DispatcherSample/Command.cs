using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherSample
{
    public abstract class Command : ICommand
    {
        public DateTime StartTime { get;  }
        public DateTime EndTime { get; internal set; }

        public ExecutionType ExecutionType { get; set; }

        public TimeSpan Runtime
        {
            get
            {
                return EndTime - StartTime;

            }
        }

        public abstract void Execute(IDispatcherContext context);

        public Command() : this(ExecutionType.Task)
        {
        }

        public Command(ExecutionType executionType)
        {
            StartTime = DateTime.UtcNow;
            ExecutionType = executionType;

        }
    }
}
