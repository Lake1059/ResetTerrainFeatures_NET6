using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResetTerrainFeatures_NET6;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;

namespace ResetTerrainFeatures_NET6.Menu
{

    public class ResetMenu : IClickableMenu
    {

        public ResetMenu(int x, int y, int width, int height) : base(Game1.viewport.Width / 2 - (600 + borderWidth * 2) / 2, Game1.viewport.Height / 2 - (660 + borderWidth * 2) / 2, 600 + borderWidth * 2, 870 + borderWidth * 2, true)
        {
            List<ClickableComponent> list = optionSlots;
            ClickableComponent clickableComponent = new(new Rectangle(xPositionOnScreen + 16, yPositionOnScreen + 40, width - 32, 40), "0")
            {
                myID = 0,
                downNeighborID = 1,
                upNeighborID = -7777,
                fullyImmutable = true
            };
            list.Add(clickableComponent);
            ClickableComponent item = new ClickableComponent(new Rectangle(xPositionOnScreen - 48, yPositionOnScreen + 80, width - 32, 40), "1");
            clickableComponent.myID = 1;
            clickableComponent.downNeighborID = 2;
            clickableComponent.upNeighborID = 0;
            clickableComponent.fullyImmutable = true;
            list.Add(item);

            options.Add(new CheckBox(i18n.ResetAllLocations, "ResetAllLocations", -1, -1, null));
            options.Add(new ResetMenuComponent(i18n.SelectObjectChanged, true));
            for (int i = 0; i < 10; i++)
            {
                list.Add(new ClickableComponent(new Rectangle(xPositionOnScreen + 16, yPositionOnScreen + 160 + i * 40, 118, 40), string.Concat(i))
                {
                    myID = i,
                    downNeighborID = list.Count + 1,
                    upNeighborID = list.Count - 1,
                    fullyImmutable = true
                });
            }
            options.Add(new CheckBox(i18n.Bush, "Bush", -1, -1, null)); //灌木丛
            options.Add(new CheckBox(i18n.Tree, "Tree", -1, -1, null)); //树木
            options.Add(new CheckBox(i18n.Weed, "Weeds", -1, -1, null)); //杂草
            options.Add(new CheckBox(i18n.Grass, "Grass", -1, -1, null)); //草料
            options.Add(new CheckBox(i18n.Twig, "Twig", -1, -1, null)); //树枝
            options.Add(new CheckBox(i18n.Rock, "Rock", -1, -1, null)); //石块
            options.Add(new CheckBox(i18n.Forage, "Forage", -1, -1, null)); //觅食
            options.Add(new CheckBox(i18n.Stump, "Stump", -1, -1, null)); //树桩
            options.Add(new CheckBox(i18n.Log, "Log", -1, -1, null)); //原木
            options.Add(new CheckBox(i18n.Boulder, "Boulder", -1, -1, null)); //巨石
            for (int j = 0; j < 6; j++)
            {
                list.Add(new ClickableComponent(new Rectangle(xPositionOnScreen + 266, yPositionOnScreen + 160 + j * 40, 118, 40), string.Concat(j))
                {
                    myID = j,
                    downNeighborID = list.Count + 1,
                    upNeighborID = list.Count - 1,
                    fullyImmutable = true
                });
            }
            options.Add(new CheckBox(i18n.Path, "Path", -1, -1, null)); //道路
            options.Add(new CheckBox(i18n.Fence, "Fence", -1, -1, null)); //围栏
            options.Add(new CheckBox(i18n.Crop, "Crop", -1, -1, null)); //作物
            options.Add(new CheckBox(i18n.TilledSoil, "Soil", -1, -1, new List<CheckBox> //耕地
			{
                options[getIndexByLabel(i18n.Crop)] as CheckBox
            }));
            options.Add(new CheckBox(i18n.Objects, "Object", -1, -1, new List<CheckBox> //对象
			{
                options[getIndexByLabel(i18n.Weed)] as CheckBox,
                options[getIndexByLabel(i18n.Fence)] as CheckBox,
                options[getIndexByLabel(i18n.Twig)] as CheckBox,
                options[getIndexByLabel(i18n.Rock)] as CheckBox,
                options[getIndexByLabel(i18n.Forage)] as CheckBox
            }));
            options.Add(new CheckBox(i18n.TFeature, "TFeature", -1, -1, new List<CheckBox> //地形特征
			{
                options[getIndexByLabel(i18n.Tree)] as CheckBox,
                options[getIndexByLabel(i18n.Grass)] as CheckBox,
                options[getIndexByLabel(i18n.Path)] as CheckBox,
                options[getIndexByLabel(i18n.TilledSoil)] as CheckBox,
                options[getIndexByLabel(i18n.Crop)] as CheckBox
            }));
            for (int k = 0; k < 3; k++)
            {
                list.Add(new ClickableComponent(new Rectangle(xPositionOnScreen + -10 + k * 200, yPositionOnScreen + 400 + 230, width - 32, 40), string.Concat(k))
                {
                    myID = k,
                    downNeighborID = 10 + k,
                    upNeighborID = 8 + k,
                    fullyImmutable = true
                });
            }
            options.Add(new ResetButton(i18n.Reset, delegate ()
            {
                resetFeatures();
            }));
            adjustSlotBounds(options.Count - 1);
            options.Add(new ResetButton(i18n.Clear, delegate ()
            {
                clearFeatures();
            }));
            adjustSlotBounds(options.Count - 1);
            options.Add(new ResetButton(i18n.Generate, delegate ()
            {
                generateFeatures();
            }));
            adjustSlotBounds(options.Count - 1);
            foreach (ResetMenuComponent resetMenuComponent in options)
            {
                if (resetMenuComponent is CheckBox && Regenerator.regeneratorOptions.ContainsKey((resetMenuComponent as CheckBox).which) && !resetMenuComponent.disabled && Regenerator.regeneratorOptions[(resetMenuComponent as CheckBox).which])
                {
                    (resetMenuComponent as CheckBox).check(true);
                }
            }
            setButtonStates();
            if (Game1.options.snappyMenus && Game1.options.gamepadControls)
            {
                base.populateClickableComponentList();
                snapToDefaultClickableComponent();
            }
        }

        public void adjustSlotBounds(int index)
        {
            Rectangle bounds = options[index].bounds;
            optionSlots[index].bounds.Width = bounds.Width;
            optionSlots[index].bounds.Height = bounds.Height;
        }

        public int getIndexByLabel(string name)
        {
            foreach (ResetMenuComponent resetMenuComponent in options)
            {
                if (resetMenuComponent.label.Equals(name))
                {
                    return options.IndexOf(resetMenuComponent);
                }
            }
            return -1;
        }

        public void resetFeatures()
        {
            if (Context.IsMainPlayer)
            {
                if (Regenerator.regeneratorOptions.ContainsKey("ResetAllLocations") && Regenerator.regeneratorOptions["ResetAllLocations"])
                {
                    foreach (GameLocation gameLocation in Game1.locations)
                    {
                        if (gameLocation.IsOutdoors)
                        {
                            //加入其他判断
                            if (ModEntry.ModC_ProtectFarm && gameLocation.IsFarm)
                            {
                                continue;
                            }
                            Regenerator.Reload(gameLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
                        }
                    }
                }
                //加入其他判断
                if (ModEntry.ModC_ProtectFarm && Game1.currentLocation.IsFarm)
                {
                    return;
                }
                Regenerator.Reload(Game1.currentLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
            }
        }

        public void clearFeatures()
        {
            if (Context.IsMainPlayer)
            {
                if (Regenerator.regeneratorOptions.ContainsKey("ResetAllLocations") && Regenerator.regeneratorOptions["ResetAllLocations"])
                {
                    foreach (GameLocation gameLocation in Game1.locations)
                    {
                        if (gameLocation.IsOutdoors)
                        {
                            //加入其他判断
                            if (ModEntry.ModC_ProtectFarm && gameLocation.IsFarm)
                            {
                                continue;
                            }
                            Regenerator.Clear(gameLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
                        }
                    }
                }
                //加入其他判断
                if (ModEntry.ModC_ProtectFarm && Game1.currentLocation.IsFarm)
                {
                    return;
                }
                Regenerator.Clear(Game1.currentLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
            }
        }

        public void generateFeatures()
        {
            if (Context.IsMainPlayer)
            {
                if (Regenerator.regeneratorOptions.ContainsKey("ResetAllLocations") && Regenerator.regeneratorOptions["ResetAllLocations"])
                {
                    foreach (GameLocation gameLocation in Game1.locations)
                    {
                        if (gameLocation.IsOutdoors)
                        {
                            //加入其他判断
                            if (ModEntry.ModC_ProtectFarm && gameLocation.IsFarm)
                            {
                                continue;
                            }
                            Regenerator.LoadMapFeatures(gameLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
                        }
                    }
                }
                //加入其他判断
                if (ModEntry.ModC_ProtectFarm && Game1.currentLocation.IsFarm)
                {
                    return;
                }
                Regenerator.LoadMapFeatures(Game1.currentLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
            }
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            base.receiveLeftClick(x, y, playSound);
            if (!GameMenu.forcePreventClose)
            {
                for (int i = 0; i < optionSlots.Count; i++)
                {
                    if (optionSlots[i].bounds.Contains(x, y) && i < options.Count && options[i].bounds.Contains(x - optionSlots[i].bounds.X, y - optionSlots[i].bounds.Y))
                    {
                        options[i].receiveLeftClick(x - optionSlots[i].bounds.X, y - optionSlots[i].bounds.Y);
                        break;
                    }
                }
                setButtonStates();
            }
        }

        public void setButtonStates()
        {
            foreach (ResetMenuComponent resetMenuComponent in options)
            {
                if (resetMenuComponent is CheckBox && Regenerator.regeneratorOptions.ContainsKey((resetMenuComponent as CheckBox).which) && !resetMenuComponent.disabled)
                {
                    bool flag = Regenerator.regeneratorOptions[(resetMenuComponent as CheckBox).which];
                    if (!(resetMenuComponent as CheckBox).isChecked && flag)
                    {
                        (resetMenuComponent as CheckBox).check(true);
                    }
                    (resetMenuComponent as CheckBox).isChecked = flag;
                }
            }
            bool flag2 = false;
            bool flag3 = false;
            foreach (ResetMenuComponent resetMenuComponent2 in options)
            {
                if (resetMenuComponent2 is CheckBox && Regenerator.canGenerate.Contains((resetMenuComponent2 as CheckBox).which) && Regenerator.regeneratorOptions.ContainsKey((resetMenuComponent2 as CheckBox).which) && Regenerator.regeneratorOptions[(resetMenuComponent2 as CheckBox).which])
                {
                    flag2 = true;
                }
            }
            foreach (ResetMenuComponent resetMenuComponent3 in options)
            {
                if (resetMenuComponent3 is CheckBox && (resetMenuComponent3 as CheckBox).which != "ResetAllLocations" && !Regenerator.canGenerate.Contains((resetMenuComponent3 as CheckBox).which) && Regenerator.regeneratorOptions.ContainsKey((resetMenuComponent3 as CheckBox).which) && Regenerator.regeneratorOptions[(resetMenuComponent3 as CheckBox).which])
                {
                    flag2 = false;
                }
                if (resetMenuComponent3 is CheckBox && (resetMenuComponent3 as CheckBox).which != "ResetAllLocations" && Regenerator.regeneratorOptions.ContainsKey((resetMenuComponent3 as CheckBox).which) && Regenerator.regeneratorOptions[(resetMenuComponent3 as CheckBox).which])
                {
                    flag3 = true;
                }
            }
            if (flag2)
            {
                options[getIndexByLabel(i18n.Reset)].disabled = false;
                options[getIndexByLabel(i18n.Generate)].disabled = false;
            }
            else
            {
                options[getIndexByLabel(i18n.Reset)].disabled = true;
                options[getIndexByLabel(i18n.Generate)].disabled = true;
            }
            if (flag3)
            {
                options[getIndexByLabel(i18n.Clear)].disabled = false;
                return;
            }
            options[getIndexByLabel(i18n.Clear)].disabled = true;
        }

        public override void leftClickHeld(int x, int y)
        {
            if (!GameMenu.forcePreventClose)
            {
                base.leftClickHeld(x, y);
                for (int i = 0; i < optionSlots.Count; i++)
                {
                    if (optionSlots[i].bounds.Contains(x, y) && i < options.Count && options[i].bounds.Contains(x - optionSlots[i].bounds.X, y - optionSlots[i].bounds.Y))
                    {
                        options[i].leftClickHeld(x - optionSlots[i].bounds.X, y - optionSlots[i].bounds.Y);
                    }
                    else
                    {
                        options[i].leftClickReleased(x - optionSlots[i].bounds.X, y - optionSlots[i].bounds.Y);
                    }
                }
            }
        }

        public override void releaseLeftClick(int x, int y)
        {
            if (!GameMenu.forcePreventClose)
            {
                base.releaseLeftClick(x, y);
                for (int i = 0; i < optionSlots.Count; i++)
                {
                    options[i].leftClickReleased(x - optionSlots[i].bounds.X, y - optionSlots[i].bounds.Y);
                }
            }
        }

        public void drawOld(SpriteBatch b)
        {
            b.End();
            b.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null);
            for (int i = 0; i < optionSlots.Count; i++)
            {
                if (i < options.Count)
                {
                    options[i].draw(b, optionSlots[i].bounds.X, optionSlots[i].bounds.Y);
                }
            }
            b.End();
            b.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            if (!hoverText.Equals(""))
            {
                drawHoverText(b, hoverText, Game1.smallFont, 0, 0, -1, null, -1, null, null, 0, null, -1, -1, -1, 1f, null, null);
            }
        }

        public override void draw(SpriteBatch b)
        {
            if (!Game1.options.showMenuBackground)
            {
                b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.75f);
            }
            drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), xPositionOnScreen, yPositionOnScreen, width, height - (256 + 40 + borderWidth * 2) + 36, Color.White, 4f, true, -1f);
            drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), xPositionOnScreen, yPositionOnScreen + height - (256 + 40 + borderWidth * 2) + 36, width, 40 + borderWidth * 2, Color.White, 4f, true, -1f);
            for (int i = 0; i < optionSlots.Count; i++)
            {
                if (i < options.Count)
                {
                    options[i].draw(b, optionSlots[i].bounds.X, optionSlots[i].bounds.Y);
                }
            }
            base.draw(b);
            drawMouse(b, false, -1);
        }

        public List<ClickableComponent> optionSlots = new List<ClickableComponent>();

        private List<ResetMenuComponent> options = new List<ResetMenuComponent>();

        private string hoverText = "";

        private const int optionsRows = 10;
    }
}
