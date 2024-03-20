using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace ResetTerrainFeatures.Menu
{
	// Token: 0x02000006 RID: 6
	public class CheckBox : ResetMenuComponent
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000037B8 File Offset: 0x000019B8
		public CheckBox(string label, string which, int x = -1, int y = -1, List<CheckBox> toDisable = null) : base(label, x, y, 36, 36)
		{
			this.which = which;
			bool flag = Regenerator.regeneratorOptions.ContainsKey(which);
			if (flag)
			{
				this.isChecked = Regenerator.regeneratorOptions[which];
			}
			else
			{
				Regenerator.regeneratorOptions[which] = false;
				this.isChecked = false;
			}
			bool flag2 = toDisable != null;
			if (flag2)
			{
				this.toDisableWhenChecked = toDisable;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000020CE File Offset: 0x000002CE
		public void disable(bool autoCheck = true)
		{
			this.disabled = true;
			this.isChecked = autoCheck;
			Regenerator.regeneratorOptions[this.which] = false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000020F1 File Offset: 0x000002F1
		public void enable()
		{
			this.disabled = false;
			this.isChecked = false;
			Regenerator.regeneratorOptions[this.which] = false;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003834 File Offset: 0x00001A34
		public override void receiveLeftClick(int x, int y)
		{
			bool flag = !this.disabled;
			if (flag)
			{
				base.receiveLeftClick(x, y);
				this.check(false);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003864 File Offset: 0x00001A64
		public void check(bool autoCheck = false)
		{
			Game1.playSound("drumkit6");
			this.isChecked = (autoCheck || !this.isChecked);
			Regenerator.regeneratorOptions[this.which] = this.isChecked;
			foreach (CheckBox item in this.toDisableWhenChecked)
			{
				bool flag = this.isChecked;
				if (flag)
				{
					item.disable(true);
				}
				else
				{
					item.enable();
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000390C File Offset: 0x00001B0C
		public override void draw(SpriteBatch b, int slotX, int slotY)
		{
			b.Draw(Game1.mouseCursors, new Vector2((float)(slotX + this.bounds.X), (float)(slotY + this.bounds.Y)), new Rectangle?(this.isChecked ? OptionsCheckbox.sourceRectChecked : OptionsCheckbox.sourceRectUnchecked), Color.White * (this.disabled ? 0.5f : 1f), 0f, Vector2.Zero, 4f, SpriteEffects.None, 0.4f);
			base.draw(b, slotX, slotY);
		}

		// Token: 0x04000009 RID: 9
		public static Rectangle sourceRectUnchecked = new Rectangle(227, 425, 9, 9);

		// Token: 0x0400000A RID: 10
		public static Rectangle sourceRectChecked = new Rectangle(236, 425, 9, 9);

		// Token: 0x0400000B RID: 11
		public const int pixelsWide = 9;

		// Token: 0x0400000C RID: 12
		public bool isChecked;

		// Token: 0x0400000D RID: 13
		public string which;

		// Token: 0x0400000E RID: 14
		public List<CheckBox> toDisableWhenChecked = new List<CheckBox>();
	}
}
