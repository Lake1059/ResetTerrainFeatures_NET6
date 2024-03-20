using System;
using StardewModdingAPI;

namespace ResetTerrainFeatures
{
	// Token: 0x02000002 RID: 2
	public static class Logger
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000021CC File Offset: 0x000003CC
		internal static void log(string log, LogLevel level = 0)
		{
			bool flag = Logger.monitor != null;
			if (flag)
			{
				Logger.monitor.Log(log, level);
			}
		}

		// Token: 0x04000001 RID: 1
		internal static IMonitor monitor;
	}
}
