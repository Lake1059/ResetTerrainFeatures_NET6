using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResetTerrainFeatures_NET6;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;

namespace ResetTerrainFeatures.Menu
{
    // Token: 0x02000008 RID: 8
    public class ResetMenu : IClickableMenu
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00003B9C File Offset: 0x00001D9C
		public ResetMenu(int x, int y, int width, int height) : base(Game1.viewport.Width / 2 - (600 + IClickableMenu.borderWidth * 2) / 2, Game1.viewport.Height / 2 - (660 + IClickableMenu.borderWidth * 2) / 2, 600 + IClickableMenu.borderWidth * 2, 870 + IClickableMenu.borderWidth * 2, true)
		{
			List<ClickableComponent> list = this.optionSlots;
			ClickableComponent clickableComponent = new(new Rectangle(this.xPositionOnScreen + 16, this.yPositionOnScreen + 40, width - 32, 40), "0")
			{
				myID = 0,
				downNeighborID = 1,
				upNeighborID = -7777,
				fullyImmutable = true
			};
			list.Add(clickableComponent);
			ClickableComponent item = new ClickableComponent(new Rectangle(this.xPositionOnScreen - 48, this.yPositionOnScreen + 80, width - 32, 40), "1");
			clickableComponent.myID = 1;
			clickableComponent.downNeighborID = 2;
			clickableComponent.upNeighborID = 0;
			clickableComponent.fullyImmutable = true;
			list.Add(item);

			this.options.Add(new CheckBox(OBJI18n.RESETALLLOCATIONS, "RESETALLLOCATIONS", -1, -1, null));
			this.options.Add(new ResetMenuComponent(OBJI18n.SelectObjectChanged, true));
			for (int i = 0; i < 10; i++)
			{
				list.Add(new ClickableComponent(new Rectangle(this.xPositionOnScreen + 16, this.yPositionOnScreen + 160 + i * 40, 118, 40), string.Concat(i))
				{
					myID = i,
					downNeighborID = list.Count + 1,
					upNeighborID = list.Count - 1,
					fullyImmutable = true
				});
			}
			this.options.Add(new CheckBox(OBJI18n.Bush, "Bush", -1, -1, null)); //灌木丛
			this.options.Add(new CheckBox(OBJI18n.Tree, "Tree", -1, -1, null)); //树木
			this.options.Add(new CheckBox(OBJI18n.Weed, "Weeds", -1, -1, null)); //杂草
			this.options.Add(new CheckBox(OBJI18n.Grass, "Grass", -1, -1, null)); //草地
			this.options.Add(new CheckBox(OBJI18n.Twig, "Twig", -1, -1, null)); //树枝
			this.options.Add(new CheckBox(OBJI18n.Rock, "Rock", -1, -1, null)); //石块
			this.options.Add(new CheckBox(OBJI18n.Forage, "Forage", -1, -1, null)); //草料
			this.options.Add(new CheckBox(OBJI18n.Stump, "Stump", -1, -1, null)); //树桩
			this.options.Add(new CheckBox(OBJI18n.Log, "Log", -1, -1, null)); //原木
			this.options.Add(new CheckBox(OBJI18n.Boulder, "Boulder", -1, -1, null)); //巨石
			for (int j = 0; j < 6; j++)
			{
				list.Add(new ClickableComponent(new Rectangle(this.xPositionOnScreen + 266, this.yPositionOnScreen + 160 + j * 40, 118, 40), string.Concat(j))
				{
					myID = j,
					downNeighborID = list.Count + 1,
					upNeighborID = list.Count - 1,
					fullyImmutable = true
				});
			}
			this.options.Add(new CheckBox(OBJI18n.Path, "Path", -1, -1, null)); //道路
			this.options.Add(new CheckBox(OBJI18n.Fence, "Fence", -1, -1, null)); //围栏
			this.options.Add(new CheckBox(OBJI18n.Crop, "Crop", -1, -1, null)); //作物
			this.options.Add(new CheckBox(OBJI18n.TilledSoil, "Soil", -1, -1, new List<CheckBox> //耕地
			{
				this.options[this.getIndexByLabel(OBJI18n.Crop)] as CheckBox
			}));
			this.options.Add(new CheckBox(OBJI18n.Objects, "Object", -1, -1, new List<CheckBox> //对象
			{
				this.options[this.getIndexByLabel(OBJI18n.Weed)] as CheckBox,
				this.options[this.getIndexByLabel(OBJI18n.Fence)] as CheckBox,
				this.options[this.getIndexByLabel(OBJI18n.Twig)] as CheckBox,
				this.options[this.getIndexByLabel(OBJI18n.Rock)] as CheckBox,
				this.options[this.getIndexByLabel(OBJI18n.Forage)] as CheckBox
			}));
			this.options.Add(new CheckBox(OBJI18n.TFeature, "TFeature", -1, -1, new List<CheckBox> //地形特征
			{
				this.options[this.getIndexByLabel(OBJI18n.Tree)] as CheckBox,
				this.options[this.getIndexByLabel(OBJI18n.Grass)] as CheckBox,
				this.options[this.getIndexByLabel(OBJI18n.Path)] as CheckBox,
				this.options[this.getIndexByLabel(OBJI18n.TilledSoil)] as CheckBox,
				this.options[this.getIndexByLabel(OBJI18n.Crop)] as CheckBox
			}));
			for (int k = 0; k < 3; k++)
			{
				list.Add(new ClickableComponent(new Rectangle(this.xPositionOnScreen + -10 + k * 200, this.yPositionOnScreen + 400 + 230, width - 32, 40), string.Concat(k))
				{
					myID = k,
					downNeighborID = 10 + k,
					upNeighborID = 8 + k,
					fullyImmutable = true
				});
			}
			this.options.Add(new ResetButton(OBJI18n.Reset, delegate()
			{
				this.resetFeatures();
			}));
			this.adjustSlotBounds(this.options.Count - 1);
			this.options.Add(new ResetButton(OBJI18n.Clear, delegate()
			{
				this.clearFeatures();
			}));
			this.adjustSlotBounds(this.options.Count - 1);
			this.options.Add(new ResetButton(OBJI18n.Generate, delegate()
			{
				this.generateFeatures();
			}));
			this.adjustSlotBounds(this.options.Count - 1);
			foreach (ResetMenuComponent resetMenuComponent in this.options)
			{
				if (resetMenuComponent is CheckBox && Regenerator.regeneratorOptions.ContainsKey((resetMenuComponent as CheckBox).which) && !resetMenuComponent.disabled && Regenerator.regeneratorOptions[(resetMenuComponent as CheckBox).which])
				{
					(resetMenuComponent as CheckBox).check(true);
				}
			}
			this.setButtonStates();
			if (Game1.options.snappyMenus && Game1.options.gamepadControls)
			{
				base.populateClickableComponentList();
				this.snapToDefaultClickableComponent();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00004300 File Offset: 0x00002500
		public void adjustSlotBounds(int index)
		{
			Rectangle bounds = this.options[index].bounds;
			this.optionSlots[index].bounds.Width = bounds.Width;
			this.optionSlots[index].bounds.Height = bounds.Height;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00004358 File Offset: 0x00002558
		public int getIndexByLabel(string name)
		{
			foreach (ResetMenuComponent resetMenuComponent in this.options)
			{
				if (resetMenuComponent.label.Equals(name))
				{
					return this.options.IndexOf(resetMenuComponent);
				}
			}
			return -1;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000043C4 File Offset: 0x000025C4
		public void resetFeatures()
		{
			if (Context.IsMainPlayer)
			{
				if (Regenerator.regeneratorOptions.ContainsKey("RESETALLLOCATIONS") && Regenerator.regeneratorOptions["RESETALLLOCATIONS"])
				{
					foreach (GameLocation gameLocation in Game1.locations)
					{
						if (gameLocation.IsOutdoors)
						{
							Regenerator.Reload(gameLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
						}
					}
				}
				Regenerator.Reload(Game1.currentLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00004464 File Offset: 0x00002664
		public void clearFeatures()
		{
			if (Context.IsMainPlayer)
			{
				if (Regenerator.regeneratorOptions.ContainsKey("RESETALLLOCATIONS") && Regenerator.regeneratorOptions["RESETALLLOCATIONS"])
				{
					foreach (GameLocation gameLocation in Game1.locations)
					{
						if (gameLocation.IsOutdoors)
						{
							Regenerator.Clear(gameLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
						}
					}
				}
				Regenerator.Clear(Game1.currentLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00004504 File Offset: 0x00002704
		public void generateFeatures()
		{
			if (Context.IsMainPlayer)
			{
				if (Regenerator.regeneratorOptions.ContainsKey("RESETALLLOCATIONS") && Regenerator.regeneratorOptions["RESETALLLOCATIONS"])
				{
					foreach (GameLocation gameLocation in Game1.locations)
					{
						if (gameLocation.IsOutdoors)
						{
							Regenerator.LoadMapFeatures(gameLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
						}
					}
				}
				Regenerator.LoadMapFeatures(Game1.currentLocation, Regenerator.GetTypesFromOptions(), Regenerator.GetIndicesFromOptions());
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000045A4 File Offset: 0x000027A4
		public override void receiveLeftClick(int x, int y, bool playSound = true)
		{
			base.receiveLeftClick(x, y, playSound);
			if (!GameMenu.forcePreventClose)
			{
				for (int i = 0; i < this.optionSlots.Count; i++)
				{
					if (this.optionSlots[i].bounds.Contains(x, y) && i < this.options.Count && this.options[i].bounds.Contains(x - this.optionSlots[i].bounds.X, y - this.optionSlots[i].bounds.Y))
					{
						this.options[i].receiveLeftClick(x - this.optionSlots[i].bounds.X, y - this.optionSlots[i].bounds.Y);
						break;
					}
				}
				this.setButtonStates();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000469C File Offset: 0x0000289C
		public void setButtonStates()
		{
			foreach (ResetMenuComponent resetMenuComponent in this.options)
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
			foreach (ResetMenuComponent resetMenuComponent2 in this.options)
			{
				if (resetMenuComponent2 is CheckBox && Regenerator.canGenerate.Contains((resetMenuComponent2 as CheckBox).which) && Regenerator.regeneratorOptions.ContainsKey((resetMenuComponent2 as CheckBox).which) && Regenerator.regeneratorOptions[(resetMenuComponent2 as CheckBox).which])
				{
					flag2 = true;
				}
			}
			foreach (ResetMenuComponent resetMenuComponent3 in this.options)
			{
				if (resetMenuComponent3 is CheckBox && (resetMenuComponent3 as CheckBox).which != "RESETALLLOCATIONS" && !Regenerator.canGenerate.Contains((resetMenuComponent3 as CheckBox).which) && Regenerator.regeneratorOptions.ContainsKey((resetMenuComponent3 as CheckBox).which) && Regenerator.regeneratorOptions[(resetMenuComponent3 as CheckBox).which])
				{
					flag2 = false;
				}
				if (resetMenuComponent3 is CheckBox && (resetMenuComponent3 as CheckBox).which != "RESETALLLOCATIONS" && Regenerator.regeneratorOptions.ContainsKey((resetMenuComponent3 as CheckBox).which) && Regenerator.regeneratorOptions[(resetMenuComponent3 as CheckBox).which])
				{
					flag3 = true;
				}
			}
			if (flag2)
			{
				this.options[this.getIndexByLabel(OBJI18n.Reset)].disabled = false;
				this.options[this.getIndexByLabel(OBJI18n.Generate)].disabled = false;
			}
			else
			{
				this.options[this.getIndexByLabel(OBJI18n.Reset)].disabled = true;
				this.options[this.getIndexByLabel(OBJI18n.Generate)].disabled = true;
			}
			if (flag3)
			{
				this.options[this.getIndexByLabel(OBJI18n.Clear)].disabled = false;
				return;
			}
			this.options[this.getIndexByLabel(OBJI18n.Clear)].disabled = true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000049A8 File Offset: 0x00002BA8
		public override void leftClickHeld(int x, int y)
		{
			if (!GameMenu.forcePreventClose)
			{
				base.leftClickHeld(x, y);
				for (int i = 0; i < this.optionSlots.Count; i++)
				{
					if (this.optionSlots[i].bounds.Contains(x, y) && i < this.options.Count && this.options[i].bounds.Contains(x - this.optionSlots[i].bounds.X, y - this.optionSlots[i].bounds.Y))
					{
						this.options[i].leftClickHeld(x - this.optionSlots[i].bounds.X, y - this.optionSlots[i].bounds.Y);
					}
					else
					{
						this.options[i].leftClickReleased(x - this.optionSlots[i].bounds.X, y - this.optionSlots[i].bounds.Y);
					}
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004ADC File Offset: 0x00002CDC
		public override void releaseLeftClick(int x, int y)
		{
			if (!GameMenu.forcePreventClose)
			{
				base.releaseLeftClick(x, y);
				for (int i = 0; i < this.optionSlots.Count; i++)
				{
					this.options[i].leftClickReleased(x - this.optionSlots[i].bounds.X, y - this.optionSlots[i].bounds.Y);
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00004B54 File Offset: 0x00002D54
		public void drawOld(SpriteBatch b)
		{
			b.End();
			b.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null);
			for (int i = 0; i < this.optionSlots.Count; i++)
			{
				if (i < this.options.Count)
				{
					this.options[i].draw(b, this.optionSlots[i].bounds.X, this.optionSlots[i].bounds.Y);
				}
			}
			b.End();
			b.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
			if (!this.hoverText.Equals(""))
			{
				IClickableMenu.drawHoverText(b, this.hoverText, Game1.smallFont, 0, 0, -1, null, -1, null, null, 0, null, -1, -1, -1, 1f, null, null);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00004C30 File Offset: 0x00002E30
		public override void draw(SpriteBatch b)
		{
			if (!Game1.options.showMenuBackground)
			{
				b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.75f);
			}
			IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), this.xPositionOnScreen, this.yPositionOnScreen, this.width, this.height - (256 + (40 + IClickableMenu.borderWidth * 2)) + 36, Color.White, 4f, true, -1f);
			IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), this.xPositionOnScreen, this.yPositionOnScreen + this.height - (256 + (40 + IClickableMenu.borderWidth * 2)) + 36, this.width, 40 + IClickableMenu.borderWidth * 2, Color.White, 4f, true, -1f);
			for (int i = 0; i < this.optionSlots.Count; i++)
			{
				if (i < this.options.Count)
				{
					this.options[i].draw(b, this.optionSlots[i].bounds.X, this.optionSlots[i].bounds.Y);
				}
			}
			base.draw(b);
			base.drawMouse(b, false, -1);
		}

		// Token: 0x04000012 RID: 18
		public List<ClickableComponent> optionSlots = new List<ClickableComponent>();

		// Token: 0x04000013 RID: 19
		private List<ResetMenuComponent> options = new List<ResetMenuComponent>();

		// Token: 0x04000014 RID: 20
		private string hoverText = "";

		// Token: 0x04000015 RID: 21
		private const int optionsRows = 10;
	}
}
