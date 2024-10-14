using System;
using StardewModdingAPI;

namespace ResetTerrainFeatures_NET6
{
    internal class ModConfig
    {
        public SButton MenuKey { get; set; } = (SButton)76;
        public bool ProtectFarm { get; set; } = true;
    }
}
