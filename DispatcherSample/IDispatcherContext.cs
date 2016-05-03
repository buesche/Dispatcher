using System.Threading;

namespace DispatcherSample
{
    public interface IDispatcherContext
    {
        CancellationToken CancellationToken { get; }
    }
}