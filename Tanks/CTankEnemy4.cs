using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CTankEnemy4 : CTankEnemy
    {

        public override void Hit(CProjectile p)
        {
            base.Hit(p);

            if (this.Armour > 0) SetImages();
        }

        public override void SetImages()
        {
            // Loading animation images:
            this.img_down = new List<Image>(CImageProvider.enemy4_down[this.Armour - 1]); 

            this.img_up = new List<Image>(CImageProvider.enemy4_up[this.Armour - 1]); 

            this.img_left = new List<Image>(CImageProvider.enemy4_left[this.Armour - 1]); 

            this.img_right = new List<Image>(CImageProvider.enemy4_right[this.Armour - 1]); 

            // Adding bonus images:
            if (IsBonus)
            {
                img_down = new List<Image>(CImageProvider.enemy4_img_down_bonus);

                img_up = new List<Image>(CImageProvider.enemy4_img_up_bonus);

                img_left = new List<Image>(CImageProvider.enemy4_img_left_bonus);

                img_right = new List<Image>(CImageProvider.enemy4_img_right_bonus);
            }

            this.Direction = Direction;
        }


        // Constructor:
        public CTankEnemy4(int x, int y, bool bonus) : base(bonus)
        {
            // Setting ID:
            this.TankID = Tanks.Enemy4;

            this.IsBonus = bonus;            

            // Setting position:
            this.X = x;
            
            this.Y = y;

            // Setting Health:
            this.Armour = 4;
            
            // Set speed:
            this.Speed = 1.0F;

            // Setting images:
            SetImages();

            // Set random vector:
            this.Direction = Directions.Down;

            this.ImageChangeInterval = 4;
        }
    }
}
