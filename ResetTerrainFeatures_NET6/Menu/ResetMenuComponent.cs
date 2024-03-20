using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewValley;

namespace ResetTerrainFeatures.Menu
{
	// Token: 0x02000009 RID: 9
	public class ResetMenuComponent
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002168 File Offset: 0x00000368
		public ResetMenuComponent(string label, bool title = false)
		{
			this.label = label;
			this.title = title;
			this.bounds = new Rectangle(32, 16, 36, 36);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public ResetMenuComponent(string label, int x, int y, int width, int height)
		{
			bool flag = x == -1;
			if (flag)
			{
				x = 32;
			}
			bool flag2 = y == -1;
			if (flag2)
			{
				y = 16;
			}
			this.bounds = new Rectangle(x, y, width, height);
			this.label = label;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000021A1 File Offset: 0x000003A1
		public ResetMenuComponent(string label, Rectangle bounds)
		{
			this.label = label;
			this.bounds = bounds;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000021C7 File Offset: 0x000003C7
		public virtual void receiveLeftClick(int x, int y)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000021C7 File Offset: 0x000003C7
		public virtual void leftClickHeld(int x, int y)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000021C7 File Offset: 0x000003C7
		public virtual void leftClickReleased(int x, int y)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000021C7 File Offset: 0x000003C7
		public virtual void receiveKeyPress(Keys key)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004E08 File Offset: 0x00003008
		public virtual void draw(SpriteBatch b, int slotX, int slotY)
		{
			Utility.drawTextWithShadow(b, this.label, Game1.dialogueFont, new Vector2((float)(slotX + this.bounds.X + this.bounds.Width + 8), (float)(slotY + this.bounds.Y)), Game1.textColor * (this.title ? 0.75f : 1f) * (this.disabled ? 0.5f : 1f), this.title ? 1.5f : 1f, 0.1f, -1, -1, 1f, 3);
		}

		// Token: 0x04000016 RID: 22
		public const int defaultX = 8;

		// Token: 0x04000017 RID: 23
		public const int defaultY = 4;

		// Token: 0x04000018 RID: 24
		public const int defaultPixelWidth = 9;

		// Token: 0x04000019 RID: 25
		public Rectangle bounds;

		// Token: 0x0400001A RID: 26
		public string label;

		// Token: 0x0400001B RID: 27
		public bool title = false;

		// Token: 0x0400001C RID: 28
		public bool disabled = false;
	}
}
