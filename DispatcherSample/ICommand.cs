using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherSample
{
    public interface ICommand
    {
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        TimeSpan Runtime { get; }

        ExecutionType ExecutionType { get; }

        void Execute(IDispatcherContext context);
    }
}
