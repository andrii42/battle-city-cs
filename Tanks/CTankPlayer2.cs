using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CTankPlayer2 : CTankPlayer
    {
        public override void Reset(bool full)
        {
            this.X = 272;
            this.Y = 400;
            base.Reset(full);
        }

        public override int TankLevel
        {
            get
            {
                return base.TankLevel;
            }
            set
            {
                base.TankLevel = value;

                SetImages();
            }
        }

        void SetImages()
        {
            switch (this.TankLevel)
            {
                case 0:
                    {
                        this.img_down = new List<Image> { Properties.Resources.player2_level0_down_1, Properties.Resources.player2_level0_down_2 };

                        this.img_up = new List<Image> { Properties.Resources.player2_level0_up_1, Properties.Resources.player2_level0_up_2 };

                        this.img_left = new List<Image> { Properties.Resources.player2_level0_left_1, Properties.Resources.player2_level0_left_2 };

                        this.img_right = new List<Image> { Properties.Resources.player2_level0_right_1, Properties.Resources.player2_level0_right_2 };

                    } break;
                case 1:
                    {
                        this.img_down = new List<Image> { Properties.Resources.player2_level1_down_1, Properties.Resources.player2_level1_down_2 };

                        this.img_up = new List<Image> { Properties.Resources.player2_level1_up_1, Properties.Resources.player2_level1_up_2 };

                        this.img_left = new List<Image> { Properties.Resources.player2_level1_left_1, Properties.Resources.player2_level1_left_2 };

                        this.img_right = new List<Image> { Properties.Resources.player2_level1_right_1, Properties.Resources.player2_level1_right_2 };

                    } break;
                case 2:
                    {
                        this.img_down = new List<Image> { Properties.Resources.player2_level2_down_1, Properties.Resources.player2_level2_down_2 };

                        this.img_up = new List<Image> { Properties.Resources.player2_level2_up_1, Properties.Resources.player2_level2_up_2 };

                        this.img_left = new List<Image> { Properties.Resources.player2_level2_left_1, Properties.Resources.player2_level2_left_2 };

                        this.img_right = new List<Image> { Properties.Resources.player2_level2_right_1, Properties.Resources.player2_level2_right_2 };

                    } break;
                case 3:
                    {
                        this.img_down = new List<Image> { Properties.Resources.player2_level3_down_1, Properties.Resources.player2_level3_down_2 };

                        this.img_up = new List<Image> { Properties.Resources.player2_level3_up_1, Properties.Resources.player2_level3_up_2 };

                        this.img_left = new List<Image> { Properties.Resources.player2_level3_left_1, Properties.Resources.player2_level3_left_2 };

                        this.img_right = new List<Image> { Properties.Resources.player2_level3_right_1, Properties.Resources.player2_level3_right_2 };

                    } break;
            }

            this.Direction = this.Direction;
        }
        
        // Constructor:
        public CTankPlayer2() : base()
        {
            // Setting ID:
            TankID = Tanks.Player2;
            
            // Loading animation images:
            /*this.img_down = new List<Image> { Properties.Resources.Player1tank_down1, Properties.Resources.Player1tank_down2 };

            this.img_up = new List<Image> { Properties.Resources.Player1tank_up1, Properties.Resources.Player1tank_up2 };

            this.img_left = new List<Image> { Properties.Resources.Player1tank_left1, Properties.Resources.Player1tank_left2 };

            this.img_right = new List<Image> { Properties.Resources.Player1tank_right1, Properties.Resources.Player1tank_right2 };*/

            SetImages();

            // Direction:            
            this.Direction = Directions.Up;

            // Coordinates:
            this.X = 272;
            
            this.Y = 400;           
        }
    }
}
