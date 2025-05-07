using System.CommandLine.Parsing;

namespace TwinCATAUTDServer;

internal enum CpuBaseTime
{
    None,
    T_1ms,
    T_500us,
    T_333us,
    T_250us,
    T_200us,
    T_125us,
    T_100us,
    T_83p3us,
    T_76p9us,
    T_71p4us,
    T_66p6us,
    T_62p5us,
    T_50us,
}

internal static class CpuBaseTimeParser
{
    private static Dictionary<string, CpuBaseTime> AvailableTime = new()
    {
        { "none", CpuBaseTime.None },
        { "1ms", CpuBaseTime.T_1ms },
        { "500us", CpuBaseTime.T_500us },
        { "333us", CpuBaseTime.T_333us },
        { "250us", CpuBaseTime.T_250us },
        { "200us", CpuBaseTime.T_200us },
        { "125us", CpuBaseTime.T_125us },
        { "100us", CpuBaseTime.T_100us },
        { "83.3us", CpuBaseTime.T_83p3us },
        { "76.9us", CpuBaseTime.T_76p9us },
        { "71.4us", CpuBaseTime.T_71p4us },
        { "66.6us", CpuBaseTime.T_66p6us },
        { "62.5us", CpuBaseTime.T_62p5us },
        { "50us", CpuBaseTime.T_50us },
    };

    internal static int ToValueUnitsOf100ns(CpuBaseTime cpuBaseTime)
    {
        return cpuBaseTime switch
        {
            CpuBaseTime.None => 0,
            CpuBaseTime.T_1ms => 10000,
            CpuBaseTime.T_500us => 5000,
            CpuBaseTime.T_333us => 3333,
            CpuBaseTime.T_250us => 2500,
            CpuBaseTime.T_200us => 2000,
            CpuBaseTime.T_125us => 1250,
            CpuBaseTime.T_100us => 1000,
            CpuBaseTime.T_83p3us => 833,
            CpuBaseTime.T_76p9us => 769,
            CpuBaseTime.T_71p4us => 714,
            CpuBaseTime.T_66p6us => 666,
            CpuBaseTime.T_62p5us => 625,
            CpuBaseTime.T_50us => 500,
            _ => throw new ArgumentOutOfRangeException(nameof(cpuBaseTime), cpuBaseTime, null),
        };
    }

    internal static CpuBaseTime Parse(ArgumentResult result)
    {
        var availableTime = string.Join(", ", AvailableTime.Select(x => x.Key));

        if (result.Tokens.Count != 1)
            throw new ArgumentException($"Expected 1 argument, but got {result.Tokens.Count}. Available options: {availableTime}.");

        var time = result.Tokens[0].Value.ToLowerInvariant();
        if (AvailableTime.TryGetValue(time, out var cpuBaseTime))
            return cpuBaseTime;
        else
            throw new ArgumentException($"Invalid CPU base time '{time}'. Available options: {availableTime}.");
    }
}


