using System;
using ResetTerrainFeatures.Menu;
using ResetTerrainFeatures_NET6;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace ResetTerrainFeatures
{
    // Token: 0x02000004 RID: 4
    public class ModEntry : Mod
	{

		// Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		public override void Entry(IModHelper helper)
		{
			Logger.monitor = base.Monitor;
			this.Config = helper.ReadConfig<ModConfig>();
			helper.Events.Input.ButtonPressed += this.ButtonPressed;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021F8 File Offset: 0x000003F8
		public void ButtonPressed(object sender, ButtonPressedEventArgs e) 
		{
			bool flag = (Game1.currentLocation != null || this.debug) && Game1.activeClickableMenu == null && e.Button == this.Config.MenuKey;
			if (flag)
			{
		 		OBJI18n.RESETALLLOCATIONS = Helper.Translation.Get("RESETALLLOCATIONS");
				OBJI18n.SelectObjectChanged = Helper.Translation.Get("SelectObjectChanged");
				OBJI18n.Bush = Helper.Translation.Get("Bush");
				OBJI18n.Tree = Helper.Translation.Get("Tree");
				OBJI18n.Weed = Helper.Translation.Get("Weed");
				OBJI18n.Grass = Helper.Translation.Get("Grass");
				OBJI18n.Twig = Helper.Translation.Get("Twig");
				OBJI18n.Rock = Helper.Translation.Get("Rock");
				OBJI18n.Forage = Helper.Translation.Get("Forage");
				OBJI18n.Stump = Helper.Translation.Get("Stump");
				OBJI18n.Log = Helper.Translation.Get("Log");
				OBJI18n.Boulder = Helper.Translation.Get("Boulder");
				OBJI18n.Path = Helper.Translation.Get("Path");
				OBJI18n.Fence = Helper.Translation.Get("Fence");
				OBJI18n.Crop = Helper.Translation.Get("Crop");
				OBJI18n.TilledSoil = Helper.Translation.Get("TilledSoil");
				OBJI18n.Objects = Helper.Translation.Get("Objects");
				OBJI18n.TFeature = Helper.Translation.Get("TFeature");
				OBJI18n.Reset = Helper.Translation.Get("Reset");
				OBJI18n.Clear = Helper.Translation.Get("Clear");
				OBJI18n.Generate = Helper.Translation.Get("Generate");
				Game1.activeClickableMenu = new ResetMenu(20, 20, 200, 200);

			}
		}

		// Token: 0x04000003 RID: 3
		public bool debug = false;

		// Token: 0x04000004 RID: 4
		private ModConfig Config;

	}

}
