using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    class CTankPortal : CAnimatable
    {
        // ********************** //
        //  Private & protected :
        // ********************** //

        // Tank that shoud appear after portal destruction:
        CTank tank;

        // ********************** //
        //  Properties :
        // ********************** //
        
        // Tank id to create:
        public Tanks TankId;

        public CTank Tank
        {
            get { return tank; }
            set { 
                  tank = value;
                  TankId = tank.TankID;
                }
        }

        // ********************** //
        //  Methods :
        // ********************** //
        // Constructor:
        public CTankPortal(int pause,int x, int y, CTank tank)
        {            
            // Setting images:
            this.images = new List<Image> { 
                                        Properties.Resources.tankportal_1, Properties.Resources.tankportal_2, Properties.Resources.tankportal_3, Properties.Resources.tankportal_4,
                                        Properties.Resources.tankportal_3, Properties.Resources.tankportal_2, Properties.Resources.tankportal_1, Properties.Resources.tankportal_2,
                                        Properties.Resources.tankportal_3, Properties.Resources.tankportal_4, Properties.Resources.tankportal_3, Properties.Resources.tankportal_2,
                                        Properties.Resources.tankportal_1, Properties.Resources.tankportal_2,Properties.Resources.tankportal_3, Properties.Resources.tankportal_4
                                      };
            // Setting tank:
            Tank = tank;
            // Setting position:
            this.X = x;
            this.Y = y;
            // Setting dimensions:
            this.Width = 32;
            this.Height = 32;
            // Setting image change interval:
            this.imageChangeInterval = 6;
            // Set it transparent:
            this.Transparent = true;
            // Pause before apearing:
            PauseBeforeApear = pause;
            //
            Visible = false;
        }
    }
}
