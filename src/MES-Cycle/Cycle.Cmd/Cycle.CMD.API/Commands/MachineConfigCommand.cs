using CQRS.Core.Commands;

namespace Cycle.Cmd.Commands;

public class MachineConfigCommand : BaseCommand
{
    // ver el json del command
    public int pressure { get; set; }
    public int grit  { get; set; }
    public int cycle_duration  { get; set; }
    public string? operator_name  { get; set; }
    public int machineIdSeq  { get; set; }
}
