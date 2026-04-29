using CQRS.Core.Commands;

namespace Cycle.Cmd.Commands;

public class NewCycleCommand : BaseCommand
{
    // ver el json del command
    public int parts_per_cycle { get; set; }
    public int finished  { get; set; }
    public string? machineConfigId  { get; set; }
    public int productionOrderId  { get; set; }
}
