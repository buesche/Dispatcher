using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherSample
{
    public class EmptyCommand : Command
    {
        public override void Execute(IDispatcherContext context)
        {
            Debug.WriteLine(this.GetType().FullName);
        }
    }
}
