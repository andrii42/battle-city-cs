using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CTankPlayer1 : CTankPlayer
    {
        public override void Reset(bool full)
        {
            this.X = 144;
            
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
                        this.img_down = new List<Image> { Properties.Resources.Player1tank_down1, Properties.Resources.Player1tank_down2 };

                        this.img_up = new List<Image> { Properties.Resources.Player1tank_up1, Properties.Resources.Player1tank_up2 };

                        this.img_left = new List<Image> { Properties.Resources.Player1tank_left1, Properties.Resources.Player1tank_left2 };

                        this.img_right = new List<Image> { Properties.Resources.Player1tank_right1, Properties.Resources.Player1tank_right2 };

                    } break;
                case 1:
                    {
                        this.img_down = new List<Image> { Properties.Resources.player2_down_1, Properties.Resources.player2_down_2 };

                        this.img_up = new List<Image> { Properties.Resources.player2_up_1, Properties.Resources.player2_up_2 };

                        this.img_left = new List<Image> { Properties.Resources.player2_left_1, Properties.Resources.player2_left_2 };

                        this.img_right = new List<Image> { Properties.Resources.player2_right_1, Properties.Resources.player2_right_2 };
                    } break;
                case 2:
                    {
                        this.img_down = new List<Image> { Properties.Resources.player3_down_1, Properties.Resources.player3_down_2 };

                        this.img_up = new List<Image> { Properties.Resources.player3_up_1, Properties.Resources.player3_up_2 };

                        this.img_left = new List<Image> { Properties.Resources.player3_left_1, Properties.Resources.player3_left_2 };

                        this.img_right = new List<Image> { Properties.Resources.player3_right_1, Properties.Resources.player3_right_2 };
                    } break;
                case 3:
                    {
                        this.img_down = new List<Image> { Properties.Resources.player4_down_1, Properties.Resources.player4_down_2 };

                        this.img_up = new List<Image> { Properties.Resources.player4_up_1, Properties.Resources.player4_up_2 };

                        this.img_left = new List<Image> { Properties.Resources.player4_left_1, Properties.Resources.player4_left_2 };

                        this.img_right = new List<Image> { Properties.Resources.player4_right_1, Properties.Resources.player4_right_2 };
                    } break;
            }

            this.Direction = this.Direction;
        }

        // Constructor:
        public CTankPlayer1():base() 
        {
            // Setting ID:
            TankID = Tanks.Player1;

            this.ImageChangeInterval = 4;

            // Loading animation images:
            SetImages();

            // Direction:            
            this.Direction = Directions.Up;

            // Coordinates:
            this.X = 144;
            
            this.Y = 400;
           
        }
    }
}
