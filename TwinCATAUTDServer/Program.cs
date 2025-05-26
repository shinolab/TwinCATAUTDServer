using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace TwinCATAUTDServer;
internal class Program
{
    [STAThread]
    private static int Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var clientIpAddr = new Option<string>(
            aliases: new[] { "--client", "-c" },
            description: "Client IP address. if empty, use localhost.",
            getDefaultValue: () => ""
        );
        var sync0CycleTime = new Option<int>(
            aliases: new[] { "--sync0", "-s" },
            description: "Sync0 cycle time in units of 500μs.",
            getDefaultValue: () => 2
        );
        var taskCycleTime = new Option<int>(
            aliases: new[] { "--task", "-t" },
            description: "Task cycle time in units of CPU base time.",
            getDefaultValue: () => 1
        );
        var cpuBaseTime = new Option<CpuBaseTime>(
            aliases: new[] { "--base", "-b" },
            description: "CPU base time.",
            parseArgument: CpuBaseTimeParser.Parse
        );
        var keep = new Option<bool>(
            aliases: new[] { "--keep", "-k" },
            description: "Keep TwinCAT XAE Shell window open",
            getDefaultValue: () => false
        );

        var rootCommand = new RootCommand("TwinCAT AUTD3 server");
        rootCommand.AddOption(clientIpAddr);
        rootCommand.AddOption(sync0CycleTime);
        rootCommand.AddOption(taskCycleTime);
        rootCommand.AddOption(cpuBaseTime);
        rootCommand.AddOption(keep);

        rootCommand.SetHandler(Setup, clientIpAddr, sync0CycleTime, taskCycleTime, cpuBaseTime, keep);

        var parser = new CommandLineBuilder(rootCommand)
        .UseDefaults()
        .UseHelp(ctx =>
        {
            ctx.HelpBuilder.CustomizeSymbol(cpuBaseTime,
                firstColumnText: (_) => $"-b, --base <{string.Join("|", CpuBaseTimeParser.AvailableTime.Select(x => x.Key))}>",
                secondColumnText: (_) => $"{cpuBaseTime.Description} [default: 1ms]"
                );
        })
        .Build();

        return parser.Invoke(args);
    }

    [STAThread]
    private static void Setup(string clientIpAddr, int sync0CycleTime, int taskCycleTime, CpuBaseTime cpuBaseTime, bool keep)
    {
        var baseTime = CpuBaseTimeParser.ToValueUnitsOf100ns(cpuBaseTime);
        (new SetupTwinCAT(clientIpAddr, baseTime * taskCycleTime, baseTime, 500000 * sync0CycleTime, keep)).Run();
    }
}
