using System;
using GenericModConfigMenu;
using Microsoft.Xna.Framework.Input;
using ResetTerrainFeatures_NET6.Menu;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace ResetTerrainFeatures_NET6
{
    public class ModEntry : Mod
    {

        public static bool ModC_ProtectFarm;
        public override void Entry(IModHelper helper)
        {
            Logger.monitor = Monitor;
            Config = helper.ReadConfig<ModConfig>();
            helper.Events.Input.ButtonPressed += ButtonPressed;
            helper.Events.GameLoop.GameLaunched += GameLaunched;
        }

        public void GameLaunched(object sender, GameLaunchedEventArgs e)
        {
            ModC_ProtectFarm = Config.ProtectFarm;
            var configMenu = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu == null)
                return;
            configMenu.Register(ModManifest, () => Config = new ModConfig(), () => Helper.WriteConfig(Config));
            configMenu.AddTextOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("GMCM_OpenMenu"),
                getValue: () => Config.MenuKey.ToString(),
                setValue: value => Config.MenuKey = (SButton)(Keys)Enum.Parse(typeof(Keys), value, true)
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("GMCM_ProtectFarm"),
                getValue: () => Config.ProtectFarm,
                setValue: value => {Config.ProtectFarm = value; ModC_ProtectFarm = value;}
            );
        }

        public void ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            bool flag = (Game1.currentLocation != null || debug) && Game1.activeClickableMenu == null && e.Button == Config.MenuKey;
            if (flag)
            {
                i18n.ResetAllLocations = Helper.Translation.Get("ResetAllLocations");
                i18n.SelectObjectChanged = Helper.Translation.Get("SelectObjectChanged");
                i18n.Bush = Helper.Translation.Get("Bush");
                i18n.Tree = Helper.Translation.Get("Tree");
                i18n.Weed = Helper.Translation.Get("Weed");
                i18n.Grass = Helper.Translation.Get("Grass");
                i18n.Twig = Helper.Translation.Get("Twig");
                i18n.Rock = Helper.Translation.Get("Rock");
                i18n.Forage = Helper.Translation.Get("Forage");
                i18n.Stump = Helper.Translation.Get("Stump");
                i18n.Log = Helper.Translation.Get("Log");
                i18n.Boulder = Helper.Translation.Get("Boulder");
                i18n.Path = Helper.Translation.Get("Path");
                i18n.Fence = Helper.Translation.Get("Fence");
                i18n.Crop = Helper.Translation.Get("Crop");
                i18n.TilledSoil = Helper.Translation.Get("TilledSoil");
                i18n.Objects = Helper.Translation.Get("Objects");
                i18n.TFeature = Helper.Translation.Get("TFeature");
                i18n.Reset = Helper.Translation.Get("Reset");
                i18n.Clear = Helper.Translation.Get("Clear");
                i18n.Generate = Helper.Translation.Get("Generate");
                Game1.activeClickableMenu = new ResetMenu(20, 20, 200, 200);

            }
        }

        public bool debug = false;

        private ModConfig Config;

    }

}
