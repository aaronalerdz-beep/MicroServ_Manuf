using CQRS.Core.Commands;

namespace Cycle.Cmd.Commands;

public interface ICommandHandler 
{
    Task HandlesAsync(NewCycleCommand command);
    Task HandlesAsync(MachineConfigCommand command);
}