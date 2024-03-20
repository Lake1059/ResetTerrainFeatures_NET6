using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace ResetTerrainFeatures.Menu
{
	// Token: 0x02000007 RID: 7
	public class ResetButton : ResetMenuComponent
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000039A0 File Offset: 0x00001BA0
		public ResetButton(string label, Action action) : base(label, false)
		{
			this.action = action;
			this.bounds = new Rectangle(32, 0, (int)Game1.dialogueFont.MeasureString(label).X + 64, 80);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003A04 File Offset: 0x00001C04
		public override void leftClickHeld(int x, int y)
		{
			bool flag = !this.disabled;
			if (flag)
			{
				this.heldDown = true;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002146 File Offset: 0x00000346
		public override void leftClickReleased(int x, int y)
		{
			this.heldDown = false;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003A28 File Offset: 0x00001C28
		public override void receiveLeftClick(int x, int y)
		{
			bool flag = this.bounds.Contains(x, y) && this.action != null && !this.disabled;
			if (flag)
			{
				Game1.playSound("Ship");
				this.action();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003A78 File Offset: 0x00001C78
		public override void draw(SpriteBatch b, int slotX, int slotY)
		{
			IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(432, 439, 9, 9), slotX + this.bounds.X, slotY + this.bounds.Y, this.bounds.Width, this.bounds.Height, (this.disabled ? this.disabledTint : Color.White) * (this.heldDown ? 0.4f : 1f), 4f, true, -1f);
			Utility.drawTextWithShadow(b, this.label, Game1.dialogueFont, new Vector2((float)(slotX + this.bounds.Center.X), (float)(slotY + this.bounds.Center.Y + 4)) - Game1.dialogueFont.MeasureString(this.label) / 2f, Game1.textColor * (this.disabled ? 0.5f : 1f), 1f, 1f, -1, -1, 0f, 3);
		}

		// Token: 0x0400000F RID: 15
		private Action action;

		// Token: 0x04000010 RID: 16
		public bool heldDown = false;

		// Token: 0x04000011 RID: 17
		public Color disabledTint = new Color(200, 200, 200);
	}
}
