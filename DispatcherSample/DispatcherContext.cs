using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DispatcherSample
{
    public class DispatcherContext : IDispatcherContext
    {
        public CancellationToken CancellationToken { get;}

        public DispatcherContext(CancellationToken cancellationToken)
        {
            CancellationToken = cancellationToken;
        }
    }
}
