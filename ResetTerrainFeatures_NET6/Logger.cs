using System;
using StardewModdingAPI;

namespace ResetTerrainFeatures_NET6
{
    public static class Logger
    {
        internal static void log(string log, LogLevel level = 0)
        {
            bool flag = monitor != null;
            if (flag)
            {
                monitor.Log(log, level);
            }
        }

        internal static IMonitor monitor;
    }
}
