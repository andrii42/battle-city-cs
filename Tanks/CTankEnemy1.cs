﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CTankEnemy1 : CTankEnemy
    {

        public override void SetImages()
        {
            // Loading animation images:
            this.img_down = new List<Image>( CImageProvider.enemy1_img_down );

            this.img_up = new List<Image>(CImageProvider.enemy1_img_up);

            this.img_left = new List<Image>( CImageProvider.enemy1_img_left); 

            this.img_right = new List<Image>( CImageProvider.enemy1_img_right); 
            
            // Adding bonus images:
            if (IsBonus)
            {
                img_down.AddRange(new List<Image>( CImageProvider.enemy1_img_down_bonus ) );

                img_up.AddRange(new List<Image>( CImageProvider.enemy1_img_up_bonus ));

                img_left.AddRange(new List<Image>( CImageProvider.enemy1_img_left_bonus ));

                img_right.AddRange(new List<Image>( CImageProvider.enemy1_img_right_bonus ));
            }
        }

        // Constructor:
        public CTankEnemy1(int x, int y, bool bonus) : base(bonus)
        {
            this.IsBonus = bonus;

            SetImages();

            // Setting ID:
            this.TankID = Tanks.Enemy1;
            
            // Set random vector:
            this.Direction = Directions.Down;

            // Setting position:
            this.X = x;
            
            this.Y = y;
            
            // Setting Health:
            this.Armour = 1;
            
            // Set speed:
            this.Speed = 1.0F;
        }

    }
}
