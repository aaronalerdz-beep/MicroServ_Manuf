using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core.Commands;

public interface ICommandHandler<in T> where T : BaseCommand
{
    Task HandleAsync(T command, CancellationToken cancellationToken = default);
}

