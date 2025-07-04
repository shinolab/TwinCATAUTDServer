﻿using System;
using System.CommandLine;
using System.Linq;

namespace TwinCATAUTDServer
{
    internal class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var clientIpAddr = new Option<string>("--client", "-c")
            {
                Description = "Client IP address. If empty, use localhost.",
                DefaultValueFactory = _ => "",
            };
            clientIpAddr.HelpName = "IP_ADDR";
            var deviceName = new Option<string>("--device_name")
            {
                Description = "Ethernet device name. If empty, use the first device found.",
                DefaultValueFactory = _ => "",
            };
            deviceName.HelpName = "DEV_NAME";
            var sync0CycleTime = new Option<int>("--sync0", "-s")
            {
                Description = "Sync0 cycle time in units of 500μs.",
                DefaultValueFactory = _ => 2,
            };
            sync0CycleTime.HelpName = "CYCLE_TIME";
            var taskCycleTime = new Option<int>("--task", "-t")
            {
                Description = "Task cycle time in units of CPU base time.",
                DefaultValueFactory = _ => 1,
            };
            taskCycleTime.HelpName = "CYCLE_TIME";
            var cpuBaseTime = new Option<string>("--base", "-b")
            {
                Description = "CPU base time.",
                DefaultValueFactory = _ => "1ms",
            };
            cpuBaseTime.AcceptOnlyFromAmong(CpuBaseTimeParser.AvailableTimes().ToArray());
            cpuBaseTime.HelpName = "TIME";
            var tcVersion = new Option<string>("--twincat")
            {
                Description = "TwinCAT version",
                DefaultValueFactory = _ => "4026",
            };
            tcVersion.AcceptOnlyFromAmong(TwinCATVersionParser.AvailableVersions().ToArray());
            var keep = new Option<bool>("--keep", "-k")
            {
                Description = "Keep TwinCAT XAE Shell window open.",
                DefaultValueFactory = _ => false
            };
            var debug = new Option<bool>("--debug", "-d")
            {
                Description = "Enable debug mode.",
                DefaultValueFactory = _ => false
            };

            var rootCommand = new RootCommand("TwinCAT AUTD3 server");
            rootCommand.Options.Add(clientIpAddr);
            rootCommand.Options.Add(deviceName);
            rootCommand.Options.Add(sync0CycleTime);
            rootCommand.Options.Add(taskCycleTime);
            rootCommand.Options.Add(cpuBaseTime);
            rootCommand.Options.Add(tcVersion);
            rootCommand.Options.Add(keep);
            rootCommand.Options.Add(debug);

            rootCommand.SetAction(parseResult =>
            {
                var clientIp = parseResult.GetValue(clientIpAddr);
                var devName = parseResult.GetValue(deviceName);
                var sync0Cycle = parseResult.GetValue(sync0CycleTime);
                var taskCycle = parseResult.GetValue(taskCycleTime);
                var baseTime = CpuBaseTimeParser.Parse(parseResult.GetResult(cpuBaseTime));
                var version = TwinCATVersionParser.Parse(parseResult.GetResult(tcVersion));
                var keepOpen = parseResult.GetValue(keep);
                var debugMode = parseResult.GetValue(debug);
                Setup(version, clientIp, devName, sync0Cycle, taskCycle, baseTime, keepOpen, debugMode);
            });

            return rootCommand.Parse(args).Invoke();
        }

        [STAThread]
        private static void Setup(TwinCATVersion version, string clientIpAddr, string deviceName, int sync0CycleTime, int taskCycleTime, CpuBaseTime cpuBaseTime, bool keep, bool debugMode)
        {
            var baseTime = CpuBaseTimeParser.ToValueUnitsOf100ns(cpuBaseTime);
            var sync0CycleTimeInNs = 500000 * sync0CycleTime;
            (new SetupTwinCAT(version, clientIpAddr, deviceName, baseTime * taskCycleTime, baseTime, sync0CycleTimeInNs, keep, debugMode)).Run();
        }
    }
}