using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DispatcherSample
{
    public class LongRunningCommand : Command
    {
        public override void Execute(IDispatcherContext context)
        {
            Debug.WriteLine(this.GetType().FullName);
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }
    }
}
