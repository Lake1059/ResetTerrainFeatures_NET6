using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace ResetTerrainFeatures_NET6.Menu
{
    public class CheckBox : ResetMenuComponent
    {
        public CheckBox(string label, string which, int x = -1, int y = -1, List<CheckBox> toDisable = null) : base(label, x, y, 36, 36)
        {
            this.which = which;
            bool flag = Regenerator.regeneratorOptions.ContainsKey(which);
            if (flag)
            {
                isChecked = Regenerator.regeneratorOptions[which];
            }
            else
            {
                Regenerator.regeneratorOptions[which] = false;
                isChecked = false;
            }
            bool flag2 = toDisable != null;
            if (flag2)
            {
                toDisableWhenChecked = toDisable;
            }
        }

        public void disable(bool autoCheck = true)
        {
            disabled = true;
            isChecked = autoCheck;
            Regenerator.regeneratorOptions[which] = false;
        }

        public void enable()
        {
            disabled = false;
            isChecked = false;
            Regenerator.regeneratorOptions[which] = false;
        }

        public override void receiveLeftClick(int x, int y)
        {
            bool flag = !disabled;
            if (flag)
            {
                base.receiveLeftClick(x, y);
                check(false);
            }
        }

        public void check(bool autoCheck = false)
        {
            Game1.playSound("drumkit6");
            isChecked = autoCheck || !isChecked;
            Regenerator.regeneratorOptions[which] = isChecked;
            foreach (CheckBox item in toDisableWhenChecked)
            {
                bool flag = isChecked;
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

        public override void draw(SpriteBatch b, int slotX, int slotY)
        {
            b.Draw(Game1.mouseCursors, new Vector2(slotX + bounds.X, slotY + bounds.Y), new Rectangle?(isChecked ? OptionsCheckbox.sourceRectChecked : OptionsCheckbox.sourceRectUnchecked), Color.White * (disabled ? 0.5f : 1f), 0f, Vector2.Zero, 4f, SpriteEffects.None, 0.4f);
            base.draw(b, slotX, slotY);
        }

        public static Rectangle sourceRectUnchecked = new Rectangle(227, 425, 9, 9);

        public static Rectangle sourceRectChecked = new Rectangle(236, 425, 9, 9);

        public const int pixelsWide = 9;

        public bool isChecked;

        public string which;

        public List<CheckBox> toDisableWhenChecked = new List<CheckBox>();
    }
}
