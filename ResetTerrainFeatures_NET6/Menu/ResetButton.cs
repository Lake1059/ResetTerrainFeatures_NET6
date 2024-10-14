using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace ResetTerrainFeatures_NET6.Menu
{
    public class ResetButton : ResetMenuComponent
    {
        public ResetButton(string label, Action action) : base(label, false)
        {
            this.action = action;
            bounds = new Rectangle(32, 0, (int)Game1.dialogueFont.MeasureString(label).X + 64, 80);
        }

        public override void leftClickHeld(int x, int y)
        {
            bool flag = !disabled;
            if (flag)
            {
                heldDown = true;
            }
        }

        public override void leftClickReleased(int x, int y)
        {
            heldDown = false;
        }

        public override void receiveLeftClick(int x, int y)
        {
            bool flag = bounds.Contains(x, y) && action != null && !disabled;
            if (flag)
            {
                Game1.playSound("Ship");
                action();
            }
        }

        public override void draw(SpriteBatch b, int slotX, int slotY)
        {
            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(432, 439, 9, 9), slotX + bounds.X, slotY + bounds.Y, bounds.Width, bounds.Height, (disabled ? disabledTint : Color.White) * (heldDown ? 0.4f : 1f), 4f, true, -1f);
            Utility.drawTextWithShadow(b, label, Game1.dialogueFont, new Vector2(slotX + bounds.Center.X, slotY + bounds.Center.Y + 4) - Game1.dialogueFont.MeasureString(label) / 2f, Game1.textColor * (disabled ? 0.5f : 1f), 1f, 1f, -1, -1, 0f, 3);
        }

        private Action action;

        public bool heldDown = false;

        public Color disabledTint = new Color(200, 200, 200);
    }
}
