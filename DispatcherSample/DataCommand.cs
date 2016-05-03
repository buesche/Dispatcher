using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherSample
{
    public class DataCommand : Command
    {
        public string Data { get; set; }
        public override void Execute(IDispatcherContext context)
        {
            Debug.WriteLine(this.GetType().FullName + " with data " + Data);
        }
    }
}
