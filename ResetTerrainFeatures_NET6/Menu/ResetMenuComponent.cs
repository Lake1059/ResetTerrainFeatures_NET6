using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewValley;

namespace ResetTerrainFeatures_NET6.Menu
{
    public class ResetMenuComponent
    {
        public ResetMenuComponent(string label, bool title = false)
        {
            this.label = label;
            this.title = title;
            bounds = new Rectangle(32, 16, 36, 36);
        }

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
            bounds = new Rectangle(x, y, width, height);
            this.label = label;
        }

        public ResetMenuComponent(string label, Rectangle bounds)
        {
            this.label = label;
            this.bounds = bounds;
        }

        public virtual void receiveLeftClick(int x, int y)
        {
        }

        public virtual void leftClickHeld(int x, int y)
        {
        }

        public virtual void leftClickReleased(int x, int y)
        {
        }

        public virtual void receiveKeyPress(Keys key)
        {
        }

        public virtual void draw(SpriteBatch b, int slotX, int slotY)
        {
            Utility.drawTextWithShadow(b, label, Game1.dialogueFont, new Vector2(slotX + bounds.X + bounds.Width + 8, slotY + bounds.Y), Game1.textColor * (title ? 0.75f : 1f) * (disabled ? 0.5f : 1f), title ? 1.5f : 1f, 0.1f, -1, -1, 1f, 3);
        }

        public const int defaultX = 8;

        public const int defaultY = 4;

        public const int defaultPixelWidth = 9;

        public Rectangle bounds;

        public string label;

        public bool title = false;

        public bool disabled = false;
    }
}
